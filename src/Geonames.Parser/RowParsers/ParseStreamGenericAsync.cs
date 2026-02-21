using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using System.Buffers;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Text;

namespace Geonames.Parser.RowParsers;

internal static class PipeReadGenericAsync
{
    public delegate TRecord? RowParserDelegate<TRecord>(ReadOnlySpan<char> span, ref ParserResult result);

    public static async Task<ParserResult> ParseStream<TRecord>(
        Stream reader,
        RowParserDelegate<TRecord> rowParser,
        Func<TRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor,
        Func<TRecord, bool>? filter,
        CancellationToken ct) where TRecord : class, IGeonameRecord
    {
        var result = new ParserResult();

        var pipeReader = PipeReader.Create(reader, new StreamPipeReaderOptions(
            bufferSize: 131072,      // 128 KB
            minimumReadSize: 65536,  // 64 KB
            leaveOpen: false
        ));

        // Rent buffers once per stream to avoid per-line overhead
        int currentByteCapacity = 4096;
        byte[] tempBytes = ArrayPool<byte>.Shared.Rent(currentByteCapacity);
        int currentCharCapacity = Encoding.UTF8.GetMaxCharCount(currentByteCapacity);
        char[] tempChars = ArrayPool<char>.Shared.Rent(currentCharCapacity);

        try
        {
            ReadResult readResult;
            do
            {
                readResult = await pipeReader.ReadAsync(ct).ConfigureAwait(false);
                ReadOnlySequence<byte> buffer = readResult.Buffer;

                while (TryGetNextLine(ref buffer, readResult.IsCompleted, out ReadOnlySequence<byte> lineBytes))
                {
                    int length = (int)lineBytes.Length;
                    if (length == 0) continue;

                    EnsureCapacity(length, ref currentByteCapacity, ref tempBytes, ref tempChars);

                    int charsCount = DecodeChars(in lineBytes, length, tempBytes, tempChars);

                    var record = rowParser(tempChars.AsSpan(0, charsCount), ref result);

                    if (IsValidRecord(record, filter))
                    {
                        result.RecordsProcessed++;
                        var added = await recordProcessor(record!, ct).ConfigureAwait(false);
                        result.RecordsAdded += added;
                    }
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);
            }
            while (!readResult.IsCompleted);

            if (finalizeProcessor is not null)
            {
                var added = await finalizeProcessor.Invoke(ct).ConfigureAwait(false);
                result.RecordsAdded += added;
            }
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
            ArrayPool<char>.Shared.Return(tempChars);
            await pipeReader.CompleteAsync().ConfigureAwait(false);
        }

        return result;
    }

    #region private helper methods

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsValidRecord<TRecord>(TRecord? record, Func<TRecord, bool>? filter)
        where TRecord : class, IGeonameRecord
    {
        if (record == null) return false;
        if (filter != null && !filter(record)) return false;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryGetNextLine(ref ReadOnlySequence<byte> buffer, bool isCompleted, out ReadOnlySequence<byte> lineBytes)
    {
        if (TryReadLine(ref buffer, out lineBytes))
        {
            return true;
        }

        if (isCompleted && buffer.Length > 0)
        {
            lineBytes = buffer;
            buffer = buffer.Slice(buffer.End);
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureCapacity(int length, ref int currentByteCapacity, ref byte[] tempBytes, ref char[] tempChars)
    {
        if (length <= currentByteCapacity) return;

        ArrayPool<byte>.Shared.Return(tempBytes);
        ArrayPool<char>.Shared.Return(tempChars);

        while (currentByteCapacity < length)
        {
            currentByteCapacity *= 2;
        }
        tempBytes = ArrayPool<byte>.Shared.Rent(currentByteCapacity);

        int currentCharCapacity = Encoding.UTF8.GetMaxCharCount(currentByteCapacity);
        tempChars = ArrayPool<char>.Shared.Rent(currentCharCapacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int DecodeChars(in ReadOnlySequence<byte> lineBytes, int length, byte[] tempBytes, char[] tempChars)
    {
        if (lineBytes.IsSingleSegment)
        {
            return Encoding.UTF8.GetChars(lineBytes.FirstSpan, tempChars);
        }

        lineBytes.CopyTo(tempBytes.AsSpan(0, length));
        return Encoding.UTF8.GetChars(tempBytes.AsSpan(0, length), tempChars);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
    {
        var reader = new SequenceReader<byte>(buffer);
        if (reader.TryReadTo(out line, (byte)'\n'))
        {
            buffer = buffer.Slice(reader.Position);
            TrimCarriageReturn(ref line);
            return true;
        }

        line = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void TrimCarriageReturn(ref ReadOnlySequence<byte> line)
    {
        int len = (int)line.Length;
        if (len == 0) return;

        if (line.IsSingleSegment)
        {
            if (line.FirstSpan[^1] == (byte)'\r')
            {
                line = line.Slice(0, len - 1);
            }
        }
        else
        {
            var pos = line.GetPosition(len - 1);
            if (line.Slice(pos, 1).FirstSpan[0] == (byte)'\r')
            {
                line = line.Slice(0, len - 1);
            }
        }
    }

    #endregion private helper methods
}

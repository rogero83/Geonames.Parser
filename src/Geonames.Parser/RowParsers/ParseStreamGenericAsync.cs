using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace Geonames.Parser.RowParsers;

internal static class PipeReadGenericAsync
{
    #region private helper methods
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
            while (true)
            {
                ReadResult readResult = await pipeReader.ReadAsync(ct).ConfigureAwait(false);
                ReadOnlySequence<byte> buffer = readResult.Buffer;

                while (true)
                {
                    bool hasLine = TryReadLine(ref buffer, out ReadOnlySequence<byte> lineBytes);

                    if (!hasLine)
                    {
                        if (readResult.IsCompleted && buffer.Length > 0)
                        {
                            lineBytes = buffer;
                            buffer = buffer.Slice(buffer.End);
                        }
                        else
                        {
                            break;
                        }
                    }

                    int length = (int)lineBytes.Length;
                    if (length == 0) continue;

                    if (length > currentByteCapacity)
                    {
                        ArrayPool<byte>.Shared.Return(tempBytes);
                        ArrayPool<char>.Shared.Return(tempChars);

                        while (currentByteCapacity < length)
                        {
                            currentByteCapacity *= 2;
                        }
                        tempBytes = ArrayPool<byte>.Shared.Rent(currentByteCapacity);

                        currentCharCapacity = Encoding.UTF8.GetMaxCharCount(currentByteCapacity);
                        tempChars = ArrayPool<char>.Shared.Rent(currentCharCapacity);
                    }

                    int charsCount;
                    if (lineBytes.IsSingleSegment)
                    {
                        charsCount = Encoding.UTF8.GetChars(lineBytes.FirstSpan, tempChars);
                    }
                    else
                    {
                        lineBytes.CopyTo(tempBytes.AsSpan(0, length));
                        charsCount = Encoding.UTF8.GetChars(tempBytes.AsSpan(0, length), tempChars);
                    }

                    var record = rowParser(tempChars.AsSpan(0, charsCount), ref result);

                    if (record != null && (filter == null || filter(record)))
                    {
                        result.RecordsProcessed++;
                        var added = await recordProcessor(record, ct).ConfigureAwait(false);
                        result.RecordsAdded += added;
                    }
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);

                if (readResult.IsCompleted)
                {
                    if (finalizeProcessor is not null)
                    {
                        var added = await finalizeProcessor.Invoke(ct).ConfigureAwait(false);
                        result.RecordsAdded += added;
                    }
                    break;
                }
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

    private static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
    {
        var reader = new SequenceReader<byte>(buffer);
        if (reader.TryReadTo(out line, (byte)'\n'))
        {
            buffer = buffer.Slice(reader.Position);

            // Handle \r
            int len = (int)line.Length;
            if (len > 0)
            {
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
            return true;
        }

        line = default;
        return false;
    }
    #endregion private helper methods
}

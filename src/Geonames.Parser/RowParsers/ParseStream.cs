using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace Geonames.Parser.RowParsers;

public static class PipeRead
{
    #region private helper methods
    public delegate TRecord? RowParserDelegate<TRecord>(ReadOnlySpan<char> span, ref ParserResult result);

    public static ParserResult ParseStream<TRecord>(
        Stream reader,
        RowParserDelegate<TRecord> rowParser,
        Func<List<TRecord>, CancellationToken, Task<int>> batchProcessor,
        Func<TRecord, bool>? filter,
        int batchSize,
        CancellationToken ct) where TRecord : class, IGeonameRecord
    {
        var result = new ParserResult();
        var batch = new List<TRecord>(batchSize);

        var pipeReader = PipeReader.Create(reader, new StreamPipeReaderOptions(
            bufferSize: 131072,      // 128 KB
            minimumReadSize: 65536,  // 64 KB
            leaveOpen: false
        ));

        try
        {
            while (true)
            {
                if (pipeReader.TryRead(out ReadResult readResult))
                {
                    ReadOnlySequence<byte> buffer = readResult.Buffer;

                    while (TryReadLine(ref buffer, out ReadOnlySequence<byte> lineBytes))
                    {
                        ProcessLine(lineBytes, result, batch, filter, rowParser, batchProcessor, batchSize, ct);
                    }

                    pipeReader.AdvanceTo(buffer.Start, buffer.End);

                    if (readResult.IsCompleted)
                    {
                        if (buffer.Length > 0)
                        {
                            ProcessLine(buffer, result, batch, filter, rowParser, batchProcessor, batchSize, ct);
                        }

                        if (batch.Count > 0)
                        {
                            ProcessBatchAsync(result, batch, batchProcessor, ct);
                        }
                        break;
                    }
                }
                else
                {
                    // Nessun dato disponibile al momento
                    Thread.Sleep(10); // Evita spin loop
                }
            }
        }
        finally
        {
            pipeReader.Complete();
        }

        return result;
    }

    private static void ProcessLine<TRecord>(
        ReadOnlySequence<byte> lineBytes,
        ParserResult result,
        List<TRecord> batch,
        Func<TRecord, bool>? filter,
        RowParserDelegate<TRecord> rowParser,
        Func<List<TRecord>, CancellationToken, Task<int>> batchProcessor,
        int batchSize,
        CancellationToken ct)
    {
        int length = (int)lineBytes.Length;
        if (length == 0) return;

        char[]? rentedArray = null;
        try
        {
            rentedArray = ArrayPool<char>.Shared.Rent(Encoding.UTF8.GetMaxCharCount(length));
            int charsCount;

            if (lineBytes.IsSingleSegment)
            {
                charsCount = Encoding.UTF8.GetChars(lineBytes.FirstSpan, rentedArray);
            }
            else
            {
                charsCount = Encoding.UTF8.GetChars(lineBytes.ToArray(), rentedArray);
            }

            //var lineSpan = rentedArray.AsSpan(0, charsCount);
            var record = rowParser(rentedArray.AsSpan(0, charsCount), ref result);

            if (record != null && (filter == null || filter(record)))
            {
                batch.Add(record);
            }

            if (batch.Count >= batchSize)
            {
                ProcessBatchAsync(result, batch, batchProcessor, ct);
            }
        }
        finally
        {
            if (rentedArray != null)
            {
                ArrayPool<char>.Shared.Return(rentedArray);
            }
        }
    }

    private static void ProcessBatchAsync<TRecord>(
        ParserResult result,
        List<TRecord> batch,
        Func<List<TRecord>, CancellationToken, Task<int>> batchProcessor,
        CancellationToken ct)
    {
        var added = Task.Run(() => batchProcessor(batch, ct)).Result;
        result.RecordsProcessed += batch.Count;
        result.RecordsAdded += added;
        batch.Clear();
    }

    private static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
    {
        SequencePosition? position = buffer.PositionOf((byte)'\n');

        if (position == null)
        {
            line = default;
            return false;
        }

        line = buffer.Slice(0, position.Value);
        buffer = buffer.Slice(buffer.GetPosition(1, position.Value));

        // Rimuovi \r se presente (gestisce sia \n che \r\n)
        if (line.Length > 0)
        {
            // Ottieni l'ultimo byte della sequenza
            byte lastByte = GetLastByte(line);
            if (lastByte == (byte)'\r')
            {
                line = line.Slice(0, line.Length - 1);
            }
        }

        return true;
    }

    private static byte GetLastByte(ReadOnlySequence<byte> sequence)
    {
        if (sequence.IsSingleSegment)
        {
            return sequence.FirstSpan[^1];
        }

        // Per sequenze multi-segmento, trova l'ultimo byte
        var position = sequence.GetPosition(sequence.Length - 1);
        return sequence.Slice(position, 1).FirstSpan[0];
    }
    #endregion private helper methods
}

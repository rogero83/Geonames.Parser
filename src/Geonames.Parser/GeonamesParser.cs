using Geonames.Parser.Contract;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using Geonames.Parser.RowParsers;
using System.Buffers;
using System.IO.Compression;
using System.IO.Pipelines;
using System.Text;

namespace Geonames.Parser;

/// <inheritdoc/>
public class GeonamesParser(IDataProcessor dataProcessor, GeonamesParserOptions? options = null)
    : IGeonamesParser
{
    private readonly IDataProcessor _dataProcessor = dataProcessor;
    private readonly GeonamesParserOptions _options = options ?? GeonamesParserOptions.Default;

    #region Country Info Parser
    /// <inheritdoc/>
    public async Task<ParserResult> ParseCountryInfoAsync(Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return await ParseCountryInfoAsync(new HttpClient(), filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseCountryInfoAsync(HttpClient systemHttpClient,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.CountryInfoUrl, ct);
        return await ParseCountryInfoAsync(stream, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseCountryInfoAsync(Stream stream,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseStream(
            stream,
            RowParser.CountryInfo,
            _dataProcessor.ProcessCountryInfoBatchAsync,
            filter,
            ct);
    }
    #endregion Country Info Parser

    #region Admin Codes Parser
    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin1CodesAsync(Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseAdmin1CodesAsync(new HttpClient(), filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAdmin1CodesAsync(HttpClient systemHttpClient, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.Admin1CodesUrl, ct);
        return await ParseAdmin1CodesAsync(stream, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin1CodesAsync(Stream stream, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseStream(
            stream,
            RowParser.Admin1Code,
            _dataProcessor.ProcessAdmin1CodeBatchAsync,
            filter,
            ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin2CodesAsync(Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseAdmin2CodesAsync(new HttpClient(), filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAdmin2CodesAsync(HttpClient systemHttpClient, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.Admin2CodesUrl, ct);
        return await ParseAdmin2CodesAsync(stream, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin2CodesAsync(Stream stream, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseStream(
            stream,
            RowParser.Admin2Code,
            _dataProcessor.ProcessAdmin2CodeBatchAsync,
            filter,
            ct);
    }
    #endregion Admin Codes Parser

    #region Geonames Parser
    /// <inheritdoc/>
    public async Task<ParserResult> ParseGeoNamesDataAsync(string isoCode, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return await ParseGeoNamesDataAsync(new HttpClient(), isoCode, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseGeoNamesDataAsync(HttpClient systemHttpClient, string isoCode, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default)
    {
        isoCode = isoCode.ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(isoCode)
            || (isoCode.Length != 2 && isoCode != "ALL"))
            return ParserResult.Error($"Invalid ISO code: {isoCode}");

        var requestUri = isoCode == "ALL"
            ? GeonamesUri.GeonamesUrl("allCountries")
            : GeonamesUri.GeonamesUrl(isoCode);

        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(requestUri, ct);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        // Find the first .txt file in the archive
        var txtEntry = archive.Entries.FirstOrDefault(entry => entry.Name == $"{isoCode}.txt");
        if (txtEntry == null)
            return ParserResult.Error($"No .txt file found for ISO code: {isoCode}");

        using var entryStream = txtEntry.Open();
        return await ParseGeoNamesDataAsync(entryStream, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseGeoNamesDataAsync(Stream stream,
        Func<GeonameRecord, bool>? filter = null,
        CancellationToken ct = default)
    {
        return ParseStream(
            stream,
            RowParser.Geonames,
            _dataProcessor.ProcessGeoNameBatchAsync,
            filter,
            ct);
    }
    #endregion Geonames Parser

    #region AlternaticeNamveV2 Parser
    /// <inheritdoc/>
    public Task<ParserResult> ParseAlternateNamesV2DataAsync(string isoCode, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseAlternateNamesV2DataAsync(new HttpClient(), isoCode, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAlternateNamesV2DataAsync(HttpClient systemHttpClient, string isoCode, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default)
    {
        isoCode = isoCode?.ToUpperInvariant() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(isoCode)
            || (isoCode.Length != 2 && isoCode != "ALL"))
            return ParserResult.Error($"Invalid ISO code: {isoCode}");

        var requestUri = isoCode == "ALL"
            ? GeonamesUri.AlternateNamesV2AllUrl()
            : GeonamesUri.AlternateNamesV2Url(isoCode);

        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(requestUri, ct);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        // Find the first .txt file in the archive
        var txtEntry = archive.Entries.FirstOrDefault(entry => entry.Name == $"{isoCode}.txt");
        if (txtEntry == null)
        {
            return ParserResult.Error($"No .txt file found for ISO code: {isoCode}");
        }

        using var entryStream = txtEntry.Open();
        return await ParseAlternateNamesV2DataAsync(entryStream, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAlternateNamesV2DataAsync(Stream stream,
        Func<AlternateNamesV2Record, bool>? filter = null,
        CancellationToken ct = default)
    {
        return ParseStream(
            stream,
            RowParser.AlternateNameV2,
            _dataProcessor.ProcessAlternateNamesV2BatchAsync,
            filter,
            ct);
    }
    #endregion AlternaticeNamveV2 Parser

    #region TimeZone Parser
    /// <inheritdoc/>
    public async Task<ParserResult> ParseTimeZoneDataAsync(Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return await ParseTimeZoneDataAsync(new HttpClient(), filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseTimeZoneDataAsync(HttpClient systemHttpClient, Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.TimeZoneUrl, ct);
        return await ParseTimeZoneDataAsync(stream, filter, ct);

    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseTimeZoneDataAsync(Stream reader, Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseStream(
            reader,
            RowParser.TimeZone,
            _dataProcessor.ProcessTimeZoneBatchAsync,
            filter,
            ct);
    }
    #endregion TimeZone Parser

    #region private helper methods
    private delegate TRecord? RowParserDelegate<TRecord>(ReadOnlySpan<char> span, ref ParserResult result);

    private async Task<ParserResult> ParseStream<TRecord>(
        Stream reader,
        RowParserDelegate<TRecord> rowParser,
        Func<List<TRecord>, CancellationToken, Task<int>> batchProcessor,
        Func<TRecord, bool>? filter,
        CancellationToken ct) where TRecord : class, IGeonameRecord
    {
        var result = new ParserResult();
        var batch = new List<TRecord>(_options.ProcessingBatchSize);

        var pipeReader = PipeReader.Create(reader, new StreamPipeReaderOptions(
            bufferSize: 131072,      // 128 KB
            minimumReadSize: 65536,  // 64 KB
            leaveOpen: false
        ));

        try
        {
            while (true)
            {
                ReadResult readResult = await pipeReader.ReadAsync(ct);
                ReadOnlySequence<byte> buffer = readResult.Buffer;

                while (TryReadLine(ref buffer, out ReadOnlySequence<byte> lineBytes))
                {
                    await ProcessLine(lineBytes, result, batch, filter, rowParser, batchProcessor, ct);
                }

                pipeReader.AdvanceTo(buffer.Start, buffer.End);

                if (readResult.IsCompleted)
                {
                    if (buffer.Length > 0)
                    {
                        await ProcessLine(buffer, result, batch, filter, rowParser, batchProcessor, ct);
                    }

                    if (batch.Count > 0)
                    {
                        await ProcessBatchAsync(result, batch, batchProcessor, ct);
                    }
                    break;
                }
            }
        }
        finally
        {
            await pipeReader.CompleteAsync();
        }

        return result;
    }

    private async Task ProcessLine<TRecord>(
        ReadOnlySequence<byte> lineBytes,
        ParserResult result,
        List<TRecord> batch,
        Func<TRecord, bool>? filter,
        RowParserDelegate<TRecord> rowParser,
        Func<List<TRecord>, CancellationToken, Task<int>> batchProcessor,
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

            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessBatchAsync(result, batch, batchProcessor, ct);
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

    private static async Task ProcessBatchAsync<TRecord>(
        ParserResult result,
        List<TRecord> batch,
        Func<List<TRecord>, CancellationToken, Task<int>> batchProcessor,
        CancellationToken ct)
    {
        var added = await batchProcessor(batch, ct);
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

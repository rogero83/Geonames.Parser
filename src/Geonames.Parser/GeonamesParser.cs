using Geonames.Parser.Contract;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using Geonames.Parser.RowParsers;
using System.Buffers;
using System.Globalization;
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
        using var reader = new StreamReader(stream);
        return await ParseCountryInfoAsync(reader, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseCountryInfoAsync(StreamReader reader,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var rowNumber = 0;
        var batch = new List<CountryInfoRecord>(_options.ProcessingBatchSize);
        string? line;
        CountryInfoRecord? record;
        while ((line = await reader.ReadLineAsync(ct)) != null)
        {
            rowNumber++;
            record = RowParser.CountryInfo(rowNumber, line, ref result);
            if (record == null)
                continue; // Skip malformed rows

            if (filter == null || filter(record))
            {
                batch.Add(record);
            }

            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessCountryInfo(result, batch, ct);

            }
        }

        if (batch.Count > 0)
        {
            await ProcessCountryInfo(result, batch, ct);
        }

        return result;
    }

    private async Task ProcessCountryInfo(ParserResult result,
        List<CountryInfoRecord> batch, CancellationToken ct)
    {
        var added = await _dataProcessor.ProcessCountryInfoBatchAsync(batch, ct);
        result.RecordsProcessed += batch.Count;
        result.RecordsAdded += added;
        batch.Clear();
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
        using var reader = new StreamReader(stream);
        return await ParseAdmin1CodesAsync(reader, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAdmin1CodesAsync(StreamReader reader, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var totalRecords = 0;
        var rowNumber = 0;
        var batch = new List<Admin1CodeRecord>(_options.ProcessingBatchSize);
        string? line;
        while ((line = await reader.ReadLineAsync(ct)) != null)
        {
            rowNumber++;
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue; // Skip empty or commented lines
            }
            totalRecords++;
            var fields = line.Split('\t');
            if (fields.Length < 4)
            {
                result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {line}");
                continue; // Not enough fields
            }

            var record = new Admin1CodeRecord
            {
                Code = fields[0],
                Name = fields[1],
                NameAscii = fields[2],
                GeonameId = int.Parse(fields[3], CultureInfo.InvariantCulture)
            };
            if (filter == null || filter(record))
            {
                batch.Add(record);
            }
            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessAdminCodesAsync(AdminLevel.Admin1, result, batch, ct);
                batch.Clear();
            }
        }
        if (batch.Count > 0)
        {
            await ProcessAdminCodesAsync(AdminLevel.Admin1, result, batch, ct);
            batch.Clear();
        }
        result.RecordsFound = totalRecords;
        return result;
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
        using var reader = new StreamReader(stream);
        return await ParseAdmin2CodesAsync(reader, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAdmin2CodesAsync(StreamReader reader, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var totalRecords = 0;
        var rowNumber = 0;
        var batch = new List<AdminXCodeRecord>(_options.ProcessingBatchSize);
        string? line;
        while ((line = await reader.ReadLineAsync(ct)) != null)
        {
            rowNumber++;
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue; // Skip empty or commented lines
            }
            totalRecords++;
            var fields = line.Split('\t');
            if (fields.Length < 4)
            {
                result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {line}");
                continue; // Not enough fields
            }

            var record = new Admin2CodeRecord
            {
                Code = fields[0],
                Name = fields[1],
                NameAscii = fields[2],
                GeonameId = int.Parse(fields[3], CultureInfo.InvariantCulture)
            };
            if (filter == null || filter(record))
            {
                batch.Add(record);
            }
            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessAdminCodesAsync(AdminLevel.Admin2, result, batch, ct);
                batch.Clear();
            }
        }
        if (batch.Count > 0)
        {
            await ProcessAdminCodesAsync(AdminLevel.Admin2, result, batch, ct);
            batch.Clear();
        }
        result.RecordsFound = totalRecords;
        return result;
    }

    private async Task ProcessAdminCodesAsync(AdminLevel level, ParserResult result, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct)
    {
        var added = await _dataProcessor.ProcessAdminCodeBatchAsync(level, batch, ct);
        result.RecordsProcessed += batch.Count();
        result.RecordsAdded += added;
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
        {
            return ParserResult.Error($"Invalid ISO code: {isoCode}");
        }

        var requestUri = isoCode == "ALL"
            ? GeonamesUri.GeonamesUrl("allCountries")
            : GeonamesUri.GeonamesUrl(isoCode);

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
        return await ParseGeoNamesDataAsync(entryStream, filter, ct);
    }

    private delegate void ProcessLineDelegate(ReadOnlySpan<char> line, bool isLastRow, CancellationToken ct);

    /// <inheritdoc/>
    public async Task<ParserResult> ParseGeoNamesDataAsync(Stream reader, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var batch = new List<GeonameRecord>(_options.ProcessingBatchSize);

        GeonameRecord? record;

        var pipeReader = PipeReader.Create(reader, new StreamPipeReaderOptions(
            bufferSize: 131072,      // 128 KB
            minimumReadSize: 65536,  // 64 KB
            leaveOpen: false
        ));

        var processLine = new ProcessLineDelegate((line, isLastRow, ct) =>
        {
            record = RowParser.Geonames(line, ref result);
            if (record != null && (filter == null || filter(record)))
            {
                batch.Add(record);
            }

            if ((batch.Count > 0 && isLastRow) || batch.Count >= _options.ProcessingBatchSize)
            {
                Task.Run(() => ProcessGeonamesAsync(result, batch, ct), ct).Wait(ct);
            }
        });

        await ProcessPipeAsync(pipeReader, processLine, ct);
        return result;
    }

    // Core processing con PipeReader
    private async Task ProcessPipeAsync(PipeReader reader,
        ProcessLineDelegate processLineAsync,
        CancellationToken ct)
    {
        try
        {
            while (true)
            {
                ReadResult result = await reader.ReadAsync(ct);
                ReadOnlySequence<byte> buffer = result.Buffer;

                while (TryReadLine(ref buffer, out ReadOnlySequence<byte> lineBytes))
                {
                    processLineAsync(GetSpanFromSequence(lineBytes), false, ct);
                }

                reader.AdvanceTo(buffer.Start, buffer.End);

                if (result.IsCompleted)
                {
                    if (buffer.Length > 0)
                    {
                        // Process last remaining line if any
                        processLineAsync(GetSpanFromSequence(buffer), true, ct);
                    }
                    else
                    {
                        // Ensure we signal the end to flush any remaining items in the batch
                        processLineAsync(string.Empty, true, ct);
                    }
                    break;
                }
            }
        }
        finally
        {
            await reader.CompleteAsync();
        }
    }

    private static ReadOnlySpan<char> GetSpanFromSequence(ReadOnlySequence<byte> sequence)
    {
        if (sequence.IsSingleSegment)
        {
            return Encoding.UTF8.GetString(sequence.FirstSpan);
        }
        return Encoding.UTF8.GetString(sequence.ToArray());
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

    private async Task ProcessGeonamesAsync(ParserResult result, List<GeonameRecord> batch, CancellationToken ct)
    {
        var added = await _dataProcessor.ProcessGeoNameBatchAsync(batch, ct);
        result.RecordsProcessed += batch.Count;
        result.RecordsAdded += added;
        batch.Clear();
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
        {
            return ParserResult.Error($"Invalid ISO code: {isoCode}");
        }

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
        using var reader = new StreamReader(entryStream);

        return await ParseAlternateNamesV2DataAsync(reader, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAlternateNamesV2DataAsync(StreamReader reader, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var totalRecords = 0;
        var rowNumber = 0;
        var batch = new List<AlternateNamesV2Record>(_options.ProcessingBatchSize);
        string? line;
        while ((line = await reader.ReadLineAsync(ct)) != null)
        {
            rowNumber++;
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue; // Skip empty or commented lines
            }
            totalRecords++;
            var fields = line.Split('\t');
            if (fields.Length < 10)
            {
                result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {line}");
                continue; // Not enough fields
            }

            var record = new AlternateNamesV2Record
            {
                AlternateNameId = int.Parse(fields[0], CultureInfo.InvariantCulture),
                GeonameId = int.Parse(fields[1], CultureInfo.InvariantCulture),
                IsoLanguage = fields[2],
                AlternateName = fields[3],
                IsPreferredName = fields[4] == "1",
                IsShortName = fields[5] == "1",
                IsColloquial = fields[6] == "1",
                IsHistoric = fields[7] == "1",
                From = FieldParser.ParseDateOnly(fields[8]),
                To = FieldParser.ParseDateOnly(fields[9])
            };

            if (filter == null || filter(record))
            {
                batch.Add(record);
            }

            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessAlternateNamesV2Async(result, batch, ct);
            }
        }

        if (batch.Count > 0)
        {
            await ProcessAlternateNamesV2Async(result, batch, ct);
        }

        result.RecordsFound = totalRecords;

        return result;
    }

    private async Task ProcessAlternateNamesV2Async(ParserResult result, List<AlternateNamesV2Record> batch, CancellationToken ct)
    {
        var added = await _dataProcessor.ProcessAlternateNamesV2BatchAsync(batch, ct);
        result.RecordsProcessed += batch.Count;
        result.RecordsAdded += added;
        batch.Clear();
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
        using var reader = new StreamReader(stream);
        return await ParseTimeZoneDataAsync(reader, filter, ct);

    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseTimeZoneDataAsync(StreamReader reader, Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var totalRecords = 0;
        var rowNumber = 0;
        var batch = new List<TimeZoneRecord>(_options.ProcessingBatchSize);
        string? line;
        while ((line = await reader.ReadLineAsync(ct)) != null)
        {
            rowNumber++;
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')
                || line.StartsWith("CountryCode")) // <= Only for this file
            {
                continue; // Skip empty or commented lines
            }
            totalRecords++;
            var fields = line.Split('\t');
            if (fields.Length < 5)
            {
                result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {line}");
                continue; // Not enough fields
            }

            var record = new TimeZoneRecord
            {
                CountryCode = fields[0],
                TimeZoneId = fields[1],
                GMTOffset = double.Parse(fields[2], CultureInfo.InvariantCulture),
                DSTOffset = double.Parse(fields[3], CultureInfo.InvariantCulture),
                RawOffset = double.Parse(fields[4], CultureInfo.InvariantCulture)
            };

            if (filter == null || filter(record))
            {
                batch.Add(record);
            }

            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessTimeZone(result, batch, ct);
            }
        }

        if (batch.Count > 0)
        {
            await ProcessTimeZone(result, batch, ct);
        }

        result.RecordsFound = totalRecords;

        return result;
    }

    private async Task ProcessTimeZone(ParserResult result, List<TimeZoneRecord> batch, CancellationToken ct)
    {
        var added = await _dataProcessor.ProcessTimeZoneBatchAsync(batch, ct);
        result.RecordsProcessed += batch.Count;
        result.RecordsAdded += added;
        batch.Clear();
    }

    #endregion TimeZone Parser
}

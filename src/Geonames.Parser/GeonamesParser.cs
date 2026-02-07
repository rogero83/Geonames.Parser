using Geonames.Parser.Contract;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using System.Globalization;
using System.IO.Compression;

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
        var totalRecords = 0;
        var rowNumber = 0;
        var batch = new List<CountryInfoRecord>(_options.ProcessingBatchSize);
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
            if (fields.Length < 18)
            {
                result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {line}");
                continue; // Not enough fields
            }

            var record = new CountryInfoRecord
            {
                ISO = fields[0],
                ISO3 = fields[1],
                ISO_Numeric = fields[2],
                Fips = fields[3],
                Country = fields[4],
                Capital = fields[5],
                Area = decimal.Parse(fields[6]),
                Population = long.Parse(fields[7]),
                Continent = fields[8],
                Tld = fields[9],
                CurrencyCode = fields[10],
                CurrencyName = fields[11],
                Phone = fields[12],
                Postal_Code_Format = fields[13],
                Postal_Code_Regex = fields[14],
                Languages = fields[15],
                GeonameId = int.Parse(fields[16]),
                Neighbours = fields[17],
                EquivalentFipsCode = fields[18]
            };

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

        result.RecordsFound = totalRecords;

        return result;
    }

    private async Task ProcessCountryInfo(ParserResult result, List<CountryInfoRecord> batch, CancellationToken ct)
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
        using var reader = new StreamReader(entryStream);

        return await ParseGeoNamesDataAsync(reader, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseGeoNamesDataAsync(StreamReader reader, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default)
    {
        var result = new ParserResult();
        var totalRecords = 0;
        var rowNumber = 0;
        var batch = new List<GeonameRecord>(_options.ProcessingBatchSize);
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
            if (fields.Length < 19)
            {
                result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {line}");
                continue; // Not enough fields
            }

            var record = new GeonameRecord
            {
                GeonameId = int.Parse(fields[0], CultureInfo.InvariantCulture),
                Name = fields[1],
                AsciiName = fields[2],
                AlternateNames = fields[3],
                Latitude = double.Parse(fields[4], CultureInfo.InvariantCulture),
                Longitude = double.Parse(fields[5], CultureInfo.InvariantCulture),
                FeatureClass = FieldParser.ParseEnum<GeonamesFeatureClass>(fields[6]),
                FeatureCode = FieldParser.ParseEnum<GeonamesFeatureCode>(fields[7]),
                CountryCode = fields[8],
                Cc2 = fields[9],
                Admin1Code = fields[10],
                Admin2Code = fields[11],
                Admin3Code = fields[12],
                Admin4Code = fields[13],
                Population = !string.IsNullOrEmpty(fields[14]) ? long.Parse(fields[14], CultureInfo.InvariantCulture) : null,
                Elevation = !string.IsNullOrEmpty(fields[15]) ? int.Parse(fields[15], CultureInfo.InvariantCulture) : null,
                Dem = !string.IsNullOrEmpty(fields[16]) ? int.Parse(fields[16], CultureInfo.InvariantCulture) : null,
                Timezone = fields[17],
                ModificationDate = FieldParser.ParseDateOnly(fields[18])
            };

            if (filter == null || filter(record))
            {
                batch.Add(record);
            }

            if (batch.Count >= _options.ProcessingBatchSize)
            {
                await ProcessGeonamesAsync(result, batch, ct);
            }
        }

        if (batch.Count > 0)
        {
            await ProcessGeonamesAsync(result, batch, ct);
        }

        result.RecordsFound = totalRecords;

        return result;
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
}

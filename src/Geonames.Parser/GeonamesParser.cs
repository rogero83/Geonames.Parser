using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using Geonames.Parser.RowParsers;
using System.IO.Compression;

namespace Geonames.Parser;

/// <inheritdoc/>
public class GeonamesParser()
    : IGeonamesParser
{
    #region Country Info Parser
    /// <inheritdoc/>
    public async Task<ParserResult> ParseCountryInfoAsync(
        Func<CountryInfoRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return await ParseCountryInfoAsync(new HttpClient(), recordProcessor, finalizeProcessor,
            filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseCountryInfoAsync(HttpClient systemHttpClient,
        Func<CountryInfoRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.CountryInfoUrl, ct);
        return await ParseCountryInfoAsync(stream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseCountryInfoAsync(Stream stream,
        Func<CountryInfoRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.CountryInfo,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }
    #endregion Country Info Parser

    #region Admin Codes Parser
    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin1CodesAsync(
        Func<Admin1CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseAdmin1CodesAsync(new HttpClient(), recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAdmin1CodesAsync(HttpClient systemHttpClient,
        Func<Admin1CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.Admin1CodesUrl, ct);
        return await ParseAdmin1CodesAsync(stream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin1CodesAsync(Stream stream,
        Func<Admin1CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.Admin1Code,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin2CodesAsync(
        Func<Admin2CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseAdmin2CodesAsync(new HttpClient(), recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAdmin2CodesAsync(HttpClient systemHttpClient,
        Func<Admin2CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.Admin2CodesUrl, ct);
        return await ParseAdmin2CodesAsync(stream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAdmin2CodesAsync(Stream stream,
        Func<Admin2CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.Admin2Code,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }
    #endregion Admin Codes Parser

    #region Geonames Parser
    /// <inheritdoc/>
    public async Task<ParserResult> ParseGeoNamesDataAsync(string isoCode,
        Func<GeonameRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return await ParseGeoNamesDataAsync(new HttpClient(), isoCode, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseGeoNamesDataAsync(HttpClient systemHttpClient, string isoCode,
        Func<GeonameRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default)
    {
        isoCode = isoCode.ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(isoCode)
            || (isoCode.Length != 2 && isoCode != "ALL"))
            return ParserResult.Error($"Invalid ISO code: {isoCode}, only ISO 2-alpha or 'ALL'");

        if (isoCode == "ALL")
            isoCode = "allCountries";

        var requestUri = GeonamesUri.GeonamesUrl(isoCode);

        using var stream = await systemHttpClient.GetStreamAsync(requestUri, ct);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        // Find the first .txt file in the archive
        var txtEntry = archive.Entries.FirstOrDefault(entry => entry.Name == $"{isoCode}.txt");
        if (txtEntry == null)
            return ParserResult.Error($"No .txt file found for ISO code: {isoCode}");
#if NET10_0_OR_GREATER
        using var entryStream = await txtEntry.OpenAsync(ct);
#else
        using var entryStream = txtEntry.Open();
#endif
        return await ParseGeoNamesDataAsync(entryStream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseGeoNamesDataAsync(Stream stream,
        Func<GeonameRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<GeonameRecord, bool>? filter = null,
        CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.Geonames,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }
    #endregion Geonames Parser

    #region AlternaticeNamveV2 Parser
    /// <inheritdoc/>
    public Task<ParserResult> ParseAlternateNamesV2DataAsync(string isoCode,
        Func<AlternateNamesV2Record, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default)
    {
        return ParseAlternateNamesV2DataAsync(new HttpClient(), isoCode, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseAlternateNamesV2DataAsync(HttpClient systemHttpClient, string isoCode,
        Func<AlternateNamesV2Record, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default)
    {
        isoCode = isoCode?.ToUpperInvariant() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(isoCode)
            || (isoCode.Length != 2 && isoCode != "ALL"))
            return ParserResult.Error($"Invalid ISO code: {isoCode}");

        string requestUri;
        if (isoCode == "ALL")
        {
            requestUri = GeonamesUri.AlternateNamesV2AllUrl();
            isoCode = "alternateNamesV2";
        }
        else
        {
            requestUri = GeonamesUri.AlternateNamesV2Url(isoCode);
        }

        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(requestUri, ct);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        // Find the first .txt file in the archive
        var txtEntry = archive.Entries.FirstOrDefault(entry => entry.Name == $"{isoCode}.txt");
        if (txtEntry == null)
        {
            return ParserResult.Error($"No .txt file found for ISO code: {isoCode}");
        }

#if NET10_0_OR_GREATER
        using var entryStream = await txtEntry.OpenAsync(ct);
#else
        using var entryStream = txtEntry.Open();
# endif        
        return await ParseAlternateNamesV2DataAsync(entryStream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseAlternateNamesV2DataAsync(Stream stream,
        Func<AlternateNamesV2Record, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<AlternateNamesV2Record, bool>? filter = null,
        CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.AlternateNameV2,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }
    #endregion AlternaticeNamveV2 Parser

    #region TimeZone Parser
    /// <inheritdoc/>
    public async Task<ParserResult> ParseTimeZoneDataAsync(
        Func<TimeZoneRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return await ParseTimeZoneDataAsync(new HttpClient(), recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParseTimeZoneDataAsync(HttpClient systemHttpClient,
        Func<TimeZoneRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        using var httpClient = systemHttpClient;
        using var stream = await httpClient.GetStreamAsync(GeonamesUri.TimeZoneUrl, ct);
        return await ParseTimeZoneDataAsync(stream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParseTimeZoneDataAsync(Stream stream,
        Func<TimeZoneRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.TimeZone,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }
    #endregion TimeZone Parser

    #region Postal Code Parser
    /// <inheritdoc/>
    public Task<ParserResult> ParsePostalCodeDataAsync(string isoCode,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParsePostalCodeDataAsync(new HttpClient(), isoCode, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParsePostalCodeDataAsync(string isoCode, bool full,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParsePostalCodeDataAsync(new HttpClient(), isoCode, full, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParsePostalCodeDataAsync(HttpClient systemHttpClient, string isoCode,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return ParsePostalCodeDataAsync(systemHttpClient, isoCode, true, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public async Task<ParserResult> ParsePostalCodeDataAsync(HttpClient systemHttpClient, string isoCode, bool full,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        isoCode = isoCode.ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(isoCode)
            || (isoCode.Length != 2 && isoCode != "ALL"))
            return ParserResult.Error($"Invalid ISO code: {isoCode}, only ISO 2-alpha or 'allCountries'");

        if (isoCode == "ALL")
            isoCode = "allCountries";

        if (full && GeonamesUri.HasFullPostalCode(isoCode))
            isoCode = $"{isoCode}_full";

        var requestUri = GeonamesUri.PostalCodeUrl(isoCode);

        using var stream = await systemHttpClient.GetStreamAsync(requestUri, ct);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);

        // Find the first .txt file in the archive
        var txtEntry = archive.Entries.FirstOrDefault(entry => entry.Name == $"{isoCode}.txt");
        if (txtEntry == null)
            return ParserResult.Error($"No .txt file found for ISO code: {isoCode}");

#if NET10_0_OR_GREATER
        using var entryStream = await txtEntry.OpenAsync(ct);
#else
        using var entryStream = txtEntry.Open();
#endif
        return await ParsePostalCodeDataAsync(entryStream, recordProcessor, finalizeProcessor, filter, ct);
    }

    /// <inheritdoc/>
    public Task<ParserResult> ParsePostalCodeDataAsync(Stream stream,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default)
    {
        return PipeReadGenericAsync.ParseStream(
            stream,
            RowParser.PostalCode,
            recordProcessor,
            finalizeProcessor,
            filter,
            ct);
    }
    #endregion Postal Code Parser
}

using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Contract.Abstractions;

public interface IGeonamesParser
{
    #region Country Info Parser
    Task<ParserResult> ParseCountryInfoAsync(Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseCountryInfoAsync(HttpClient systemHttpClient, Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseCountryInfoAsync(StreamReader reader, Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Country Info Parser

    #region Admin Codes Parser
    Task<ParserResult> ParseAdmin1CodesAsync(Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseAdmin1CodesAsync(HttpClient systemHttpClient, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseAdmin1CodesAsync(StreamReader reader, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    Task<ParserResult> ParseAdmin2CodesAsync(Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseAdmin2CodesAsync(HttpClient systemHttpClient, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseAdmin2CodesAsync(StreamReader reader, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Admin Codes Parser

    #region Geonames Parser
    Task<ParserResult> ParseGeoNamesDataAsync(string isoCode, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseGeoNamesDataAsync(HttpClient systemHttpClient, string isoCode, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseGeoNamesDataAsync(StreamReader reader, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Geonames Parser

    #region AlternaticeNamveV2 Parser
    Task<ParserResult> ParseAlternateNamesV2DataAsync(string isoCode, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseAlternateNamesV2DataAsync(HttpClient systemHttpClient, string isoCode, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);
    Task<ParserResult> ParseAlternateNamesV2DataAsync(StreamReader reader, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);
    #endregion AlternaticeNamveV2 Parser
}
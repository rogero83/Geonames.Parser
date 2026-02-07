using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Contract.Abstractions;

/// <summary>
/// Provides methods for parsing and processing GeoNames data files, including country information, administrative
/// codes, geonames records, and alternate names V2, with support for filtering and batch processing.
/// </summary>
/// <remarks>The GeonamesParser class enables asynchronous parsing of various GeoNames datasets from streams or
/// remote sources, applying optional filters and batching records for efficient processing. It is designed to work with
/// an IDataProcessor implementation to handle parsed data. Thread safety depends on the underlying IDataProcessor and
/// HttpClient usage. For optimal performance, reuse HttpClient instances when possible and avoid sharing a single
/// GeonamesParser instance across concurrent operations unless the provided IDataProcessor is thread-safe.</remarks>
public interface IGeonamesParser
{
    #region Country Info Parser
    /// <summary>
    /// Parses country information records from a remote source.
    /// </summary>
    /// <param name="filter">An optional predicate used to filter each parsed <see cref="CountryInfoRecord"/>. Only records for which the
    /// predicate returns <see langword="true"/> are included. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/>
    /// with the filtered country information records.</returns>
    Task<ParserResult> ParseCountryInfoAsync(Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    /// <summary>
    /// Parses country information records from a remote source.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client instance used to send requests to the remote service. Must not be null.</param>
    /// <param name="filter">An optional predicate to filter the parsed country records. Only records for which the predicate returns <see
    /// langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ParserResult"/> with
    /// the parsed country information records.</returns>
    Task<ParserResult> ParseCountryInfoAsync(HttpClient systemHttpClient, Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    /// <summary>
    /// Parses country information records from the provided stream.
    /// </summary>
    /// <param name="reader">A StreamReader that supplies the input data to parse. The stream must be readable and positioned at the start of
    /// the data to be parsed.</param>
    /// <param name="filter">An optional predicate used to filter parsed CountryInfoRecord objects. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a ParserResult with the parsed
    /// and filtered country information records.</returns>
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
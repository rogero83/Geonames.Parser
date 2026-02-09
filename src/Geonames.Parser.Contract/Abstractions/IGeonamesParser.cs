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
    /// <summary>
    /// Parses administrative level 1 records (provinces, states, etc.) from a remote source.
    /// </summary>
    /// <param name="filter">An optional predicate used to filter each parsed <see cref="Admin1CodeRecord"/>. Only records for which the
    /// predicate returns <see langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/>
    /// with the filtered administrative level 1 records.</returns>
    Task<ParserResult> ParseAdmin1CodesAsync(Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 1 records (provinces, states, etc.) from a remote source.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client instance used to send requests to the remote service. Must not be null.</param>
    /// <param name="filter">An optional predicate to filter the parsed records. Only records for which the predicate returns <see
    /// langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ParserResult"/> with
    /// the parsed administrative level 1 records.</returns>
    Task<ParserResult> ParseAdmin1CodesAsync(HttpClient systemHttpClient, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 1 records (provinces, states, etc.) from the provided stream.
    /// </summary>
    /// <param name="reader">A StreamReader that supplies the input data to parse. The stream must be readable and positioned at the start of
    /// the data to be parsed.</param>
    /// <param name="filter">An optional predicate used to filter parsed records. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/> with the parsed
    /// and filtered administrative level 1 records.</returns>
    Task<ParserResult> ParseAdmin1CodesAsync(StreamReader reader, Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 2 records (counties, districts, etc.) from a remote source.
    /// </summary>
    /// <param name="filter">An optional predicate used to filter each parsed <see cref="Admin2CodeRecord"/>. Only records for which the
    /// predicate returns <see langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/>
    /// with the filtered administrative level 2 records.</returns>
    Task<ParserResult> ParseAdmin2CodesAsync(Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 2 records (counties, districts, etc.) from a remote source.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client instance used to send requests to the remote service. Must not be null.</param>
    /// <param name="filter">An optional predicate to filter the parsed records. Only records for which the predicate returns <see
    /// langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ParserResult"/> with
    /// the parsed administrative level 2 records.</returns>
    Task<ParserResult> ParseAdmin2CodesAsync(HttpClient systemHttpClient, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 2 records (counties, districts, etc.) from the provided stream.
    /// </summary>
    /// <param name="reader">A StreamReader that supplies the input data to parse. The stream must be readable and positioned at the start of
    /// the data to be parsed.</param>
    /// <param name="filter">An optional predicate used to filter parsed records. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/> with the parsed
    /// and filtered administrative level 2 records.</returns>
    Task<ParserResult> ParseAdmin2CodesAsync(StreamReader reader, Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Admin Codes Parser

    #region Geonames Parser
    /// <summary>
    /// Parses GeoNames records for a specified ISO country code from a remote source.
    /// </summary>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US", "IT"). Use "ALL" to parse all GeoNames records.</param>
    /// <param name="filter">An optional predicate used to filter each parsed <see cref="GeonameRecord"/>. Only records for which the
    /// predicate returns <see langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/>
    /// with the filtered GeoNames records.</returns>
    Task<ParserResult> ParseGeoNamesDataAsync(string isoCode, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses GeoNames records for a specified ISO country code from a remote source.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client instance used to send requests to the remote service. Must not be null.</param>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US", "IT"). Use "ALL" to parse all GeoNames records.</param>
    /// <param name="filter">An optional predicate used to filter each parsed <see cref="GeonameRecord"/>. Only records for which the
    /// predicate returns <see langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/>
    /// with the filtered GeoNames records.</returns>
    Task<ParserResult> ParseGeoNamesDataAsync(HttpClient systemHttpClient, string isoCode, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses GeoNames records from the provided stream.
    /// </summary>
    /// <param name="reader">A StreamReader that supplies the input data to parse. The stream must be readable and positioned at the start of
    /// the data to be parsed.</param>
    /// <param name="filter">An optional predicate used to filter parsed records. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous parse operation. The task result contains a <see cref="ParserResult"/> with the parsed
    /// and filtered GeoNames records.</returns>
    Task<ParserResult> ParseGeoNamesDataAsync(Stream reader, Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Geonames Parser

    #region AlternateNamesV2 Parser
    /// <summary>
    /// Parses alternate names V2 records for a specified ISO country code, applying an optional filter to include only relevant records.<br/>
    /// Use ISO = "ALL" to parse all alternate names V2 records regardless of country code.
    /// </summary>
    /// <param name="isoCode">ISO or ALL</param>
    /// <param name="filter">An optional predicate used to filter parsed AlternateNamesV2Record objects. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>The task result contains a <see cref="ParserResult"/> with the filtered time zone records.</returns>
    Task<ParserResult> ParseAlternateNamesV2DataAsync(string isoCode, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses alternate names V2 records for a specified ISO country code from a remote source.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client instance used to send requests to the remote service. Must not be null.</param>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US", "IT"). Use "ALL" to parse all alternate names data.</param>
    /// <param name="filter">An optional predicate used to filter parsed <see cref="AlternateNamesV2Record"/> objects. Only records for which the predicate
    /// returns <see langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>The task result contains a <see cref="ParserResult"/> with the filtered time zone records.</returns>
    Task<ParserResult> ParseAlternateNamesV2DataAsync(HttpClient systemHttpClient, string isoCode, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses alternate names V2 records from the provided stream.
    /// </summary>
    /// <param name="reader">A StreamReader that supplies the input data to parse. The stream must be readable and positioned at the start of
    /// the data to be parsed.</param>
    /// <param name="filter">An optional predicate used to filter parsed records. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>The task result contains a <see cref="ParserResult"/> with the filtered time zone records.</returns>
    Task<ParserResult> ParseAlternateNamesV2DataAsync(StreamReader reader, Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);
    #endregion AlternateNamesV2 Parser

    #region TimeZone Parser
    /// <summary>
    /// Parses time zone records from a remote source.
    /// </summary>
    /// <param name="filter">An optional predicate used to filter each parsed <see cref="TimeZoneRecord"/>. Only records for which the
    /// predicate returns <see langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The task result contains a <see cref="ParserResult"/> with the filtered time zone records.</returns>
    Task<ParserResult> ParseTimeZoneDataAsync(Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses time zone records from a remote source.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client instance used to send requests to the remote service. Must not be null.</param>
    /// <param name="filter">An optional predicate used to filter parsed <see cref="TimeZoneRecord"/> records. Only records for which the predicate returns <see
    /// langword="true"/> are included. If null, all records are included.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The task result contains a <see cref="ParserResult"/> with the filtered time zone records.</returns>
    Task<ParserResult> ParseTimeZoneDataAsync(HttpClient systemHttpClient, Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses time zone records from the provided stream.
    /// </summary>
    /// <param name="reader">A StreamReader that supplies the input data to parse. The stream must be readable and positioned at the start of
    /// the data to be parsed.</param>
    /// <param name="filter">An optional predicate used to filter parsed records. Only records for which the predicate
    /// returns <see langword="true"/> are included in the result. If null, all records are included.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>The task result contains a <see cref="ParserResult"/> with the filtered time zone records.</returns>
    Task<ParserResult> ParseTimeZoneDataAsync(StreamReader reader, Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion TimeZone Parser
}
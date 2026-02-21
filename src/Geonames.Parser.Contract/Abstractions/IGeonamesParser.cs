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
    /// <param name="recordProcessor">A function to process each parsed <see cref="CountryInfoRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseCountryInfoAsync(
        Func<CountryInfoRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    /// <summary>
    /// Parses country information records from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="CountryInfoRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseCountryInfoAsync(HttpClient systemHttpClient,
        Func<CountryInfoRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    /// <summary>
    /// Parses country information records from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="CountryInfoRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseCountryInfoAsync(Stream stream,
        Func<CountryInfoRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<CountryInfoRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Country Info Parser

    #region Admin Codes Parser
    /// <summary>
    /// Parses administrative level 1 records (provinces, states, etc.) from a remote source.
    /// </summary>
    /// <param name="recordProcessor">A function to process each parsed <see cref="Admin1CodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAdmin1CodesAsync(
        Func<Admin1CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 1 records (provinces, states, etc.) from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="Admin1CodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAdmin1CodesAsync(HttpClient systemHttpClient,
        Func<Admin1CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 1 records (provinces, states, etc.) from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="Admin1CodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAdmin1CodesAsync(Stream stream,
        Func<Admin1CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin1CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 2 records (counties, districts, etc.) from a remote source.
    /// </summary>
    /// <param name="recordProcessor">A function to process each parsed <see cref="Admin2CodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAdmin2CodesAsync(
        Func<Admin2CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 2 records (counties, districts, etc.) from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="Admin2CodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAdmin2CodesAsync(HttpClient systemHttpClient,
        Func<Admin2CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses administrative level 2 records (counties, districts, etc.) from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="Admin2CodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAdmin2CodesAsync(Stream stream,
        Func<Admin2CodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<Admin2CodeRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Admin Codes Parser

    #region Geonames Parser
    /// <summary>
    /// Parses GeoNames records for a specified ISO country code from a remote source.
    /// </summary>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="GeonameRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseGeoNamesDataAsync(string isoCode,
        Func<GeonameRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<GeonameRecord, bool>? filter = null,
        CancellationToken ct = default);

    /// <summary>
    /// Parses GeoNames records for a specified ISO country code from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="GeonameRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseGeoNamesDataAsync(HttpClient systemHttpClient, string isoCode,
        Func<GeonameRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<GeonameRecord, bool>? filter = null,
        CancellationToken ct = default);

    /// <summary>
    /// Parses GeoNames records from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="GeonameRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseGeoNamesDataAsync(Stream stream,
        Func<GeonameRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<GeonameRecord, bool>? filter = null, CancellationToken ct = default);

    #endregion Geonames Parser

    #region AlternateNamesV2 Parser
    /// <summary>
    /// Parses alternate names V2 records for a specified ISO country code from a remote source.
    /// </summary>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="AlternateNamesV2Record"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAlternateNamesV2DataAsync(string isoCode,
        Func<AlternateNamesV2Record, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses alternate names V2 records for a specified ISO country code from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="AlternateNamesV2Record"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAlternateNamesV2DataAsync(HttpClient systemHttpClient, string isoCode,
        Func<AlternateNamesV2Record, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses alternate names V2 records from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="AlternateNamesV2Record"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseAlternateNamesV2DataAsync(Stream stream,
        Func<AlternateNamesV2Record, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<AlternateNamesV2Record, bool>? filter = null, CancellationToken ct = default);
    #endregion AlternateNamesV2 Parser

    #region TimeZone Parser
    /// <summary>
    /// Parses time zone records from a remote source.
    /// </summary>
    /// <param name="recordProcessor">A function to process each parsed <see cref="TimeZoneRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseTimeZoneDataAsync(
        Func<TimeZoneRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses time zone records from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="TimeZoneRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseTimeZoneDataAsync(HttpClient systemHttpClient,
        Func<TimeZoneRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses time zone records from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="TimeZoneRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParseTimeZoneDataAsync(Stream stream,
        Func<TimeZoneRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<TimeZoneRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion TimeZone Parser

    #region Postal Code Parser
    /// <summary>
    /// Parses postal code records for a specified ISO country code from a remote source.
    /// </summary>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="PostalCodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParsePostalCodeDataAsync(string isoCode,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses postal code records for a specified ISO country code from a remote source, with an option to request full precision data.
    /// </summary>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="full">If <see langword="true"/>, attempts to fetch full precision postal code data where available.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="PostalCodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParsePostalCodeDataAsync(string isoCode, bool full,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses postal code records for a specified ISO country code from a remote source using a specific HTTP client.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="PostalCodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParsePostalCodeDataAsync(HttpClient systemHttpClient, string isoCode,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses postal code records for a specified ISO country code from a remote source using a specific HTTP client, with an option to request full precision data.
    /// </summary>
    /// <param name="systemHttpClient">The HTTP client used to send requests. Must not be null.</param>
    /// <param name="isoCode">The ISO 3166-1 alpha-2 country code (e.g., "US"). Use "ALL" for all records.</param>
    /// <param name="full">If <see langword="true"/>, attempts to fetch full precision postal code data where available.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="PostalCodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParsePostalCodeDataAsync(HttpClient systemHttpClient, string isoCode, bool full,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default);

    /// <summary>
    /// Parses postal code records from the provided stream.
    /// </summary>
    /// <param name="stream">A stream containing the source data. Must be readable and positioned at the start.</param>
    /// <param name="recordProcessor">A function to process each parsed <see cref="PostalCodeRecord"/>. It receives the record and a cancellation token, and returns the number of processed items.</param>
    /// <param name="finalizeProcessor">An optional function to perform final actions after all records have been parsed. It receives a cancellation token and returns the number of processed items.</param>
    /// <param name="filter">An optional predicate to filter records. Only records that match the filter are processed. If <see langword="null"/>, all records are included.</param>
    /// <param name="ct">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The result contains a <see cref="ParserResult"/> with the execution details.</returns>
    Task<ParserResult> ParsePostalCodeDataAsync(Stream stream,
        Func<PostalCodeRecord, CancellationToken, Task<int>> recordProcessor,
        Func<CancellationToken, Task<int>>? finalizeProcessor = null,
        Func<PostalCodeRecord, bool>? filter = null, CancellationToken ct = default);
    #endregion Postal Code Parser
}
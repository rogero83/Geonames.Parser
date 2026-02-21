using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Contract.Abstractions;

/// <summary>
/// Defines methods for processing data records asynchronously.
/// </summary>
/// <remarks>Implementations of this interface are responsible for handling bulk operations. Methods are
/// designed to support asynchronous processing and cancellation via a <see cref="CancellationToken"/>. This interface
/// is intended for use in scenarios where large datasets need to be ingested, transformed, or stored
/// efficiently.</remarks>
public interface IDataProcessor
{
#pragma warning disable CS1591
    Task<int> ProcessCountryInfoRecordAsync(CountryInfoRecord record, CancellationToken ct = default);
    Task<int> FinalizeCountryInfoRecordAsync(CancellationToken ct = default);

    Task<int> ProcessAdmin1CodeRecordAsync(Admin1CodeRecord record, CancellationToken ct = default);
    Task<int> FinalizeAdmin1CodeRecordAsync(CancellationToken ct = default);

    Task<int> ProcessAdmin2CodeRecordAsync(Admin2CodeRecord record, CancellationToken ct = default);
    Task<int> FinalizeAdmin2CodeRecordAsync(CancellationToken ct = default);

    Task<int> ProcessGeoNameRecordAsync(GeonameRecord record, CancellationToken ct = default);
    Task<int> FinalizeGeoNameRecordAsync(CancellationToken ct = default);

    Task<int> ProcessAlternateNamesV2RecordAsync(AlternateNamesV2Record record, CancellationToken ct = default);
    Task<int> FinalizeAlternateNamesV2RecordAsync(CancellationToken ct = default);

    Task<int> ProcessTimeZoneRecordAsync(TimeZoneRecord record, CancellationToken ct = default);
    Task<int> FinalizeTimeZoneRecordAsync(CancellationToken ct = default);

    Task<int> ProcessPostalCodeRecordAsync(PostalCodeRecord record, CancellationToken ct = default);
    Task<int> FinalizePostalCodeRecordAsync(CancellationToken ct = default);
#pragma warning restore CS1591
}

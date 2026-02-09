using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Contract.Abstractions;

/// <summary>
/// Defines methods for processing batches of data records asynchronously.
/// </summary>
/// <remarks>Implementations of this interface are responsible for handling bulk operations. Methods are
/// designed to support asynchronous processing and cancellation via a <see cref="CancellationToken"/>. This interface
/// is intended for use in scenarios where large datasets need to be ingested, transformed, or stored
/// efficiently.</remarks>
public interface IDataProcessor
{
#pragma warning disable CS1591
    Task<int> ProcessCountryInfoBatchAsync(IEnumerable<CountryInfoRecord> reader, CancellationToken ct = default);
    Task<int> ProcessAdmin1CodeBatchAsync(IEnumerable<Admin1CodeRecord> batch, CancellationToken ct = default);
    Task<int> ProcessAdmin2CodeBatchAsync(IEnumerable<Admin2CodeRecord> batch, CancellationToken ct = default);
    Task<int> ProcessGeoNameBatchAsync(IEnumerable<GeonameRecord> batch, CancellationToken ct = default);
    Task<int> ProcessAlternateNamesV2BatchAsync(IEnumerable<AlternateNamesV2Record> batch, CancellationToken ct = default);
    Task<int> ProcessTimeZoneBatchAsync(IEnumerable<TimeZoneRecord> batch, CancellationToken ct = default);
#pragma warning restore CS1591
}

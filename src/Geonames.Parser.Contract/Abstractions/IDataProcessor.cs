using Geonames.Parser.Contract.Enums;
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
    Task<int> ProcessCountryInfoBatchAsync(IEnumerable<CountryInfoRecord> reader, CancellationToken ct = default);

    Task<int> ProcessAdminCodeBatchAsync(AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct = default);

    Task<int> ProcessGeoNameBatchAsync(IEnumerable<GeonameRecord> batch, CancellationToken ct = default);

    Task<int> ProcessAlternateNamesV2BatchAsync(IEnumerable<AlternateNamesV2Record> batch, CancellationToken ct = default);
}

using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Contract.Abstractions;

public interface IDataProcessor
{
    Task<int> ProcessCountryInfoBatchAsync(ICollection<CountryInfoRecord> reader, CancellationToken ct = default);

    Task<int> ProcessAdminCodeBatchAsync(AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct = default);

    Task<int> ProcessGeoNameBatchAsync(ICollection<GeonameRecord> batch, CancellationToken ct = default);

    Task<int> ProcessAlternateNamesV2BatchAsync(ICollection<AlternateNamesV2Record> batch, CancellationToken ct = default);
}

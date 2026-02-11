using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Console;

internal class ConsoleDataProcessor : IDataProcessor
{
    public async Task<int> ProcessCountryInfoBatchAsync(IEnumerable<CountryInfoRecord> reader, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in reader)
        {
            if (record != null)
            {
                rowNumber++;
                System.Console.WriteLine($"{rowNumber} Country: {record.Country}, Capital: {record.Capital}, Population: {record.Population}");
            }
        }
        return rowNumber;
    }

    public async Task<int> ProcessAdmin1CodeBatchAsync(IEnumerable<Admin1CodeRecord> batch, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} CountryCode: {record.CountryCode}, Admin1: {record.Admin1Code}, Name: {record.Name}, NameAscii: {record.NameAscii}, GeonameId: {record.GeonameId}");
        }
        return batch.Count();
    }

    public async Task<int> ProcessAdmin2CodeBatchAsync(IEnumerable<Admin2CodeRecord> batch, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} CountryCode: {record.CountryCode}, Admin1: {record.Admin1Code}, Admin2: {record.Admin2Code}, Name: {record.Name}, NameAscii: {record.NameAscii}, GeonameId: {record.GeonameId}");
        }
        return batch.Count();
    }

    public async Task<int> ProcessGeoNameBatchAsync(IEnumerable<GeonameRecord> batch, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} GeoNameId: {record.GeonameId}, Name: {record.Name}, CountryCode: {record.CountryCode}, Latitude: {record.Latitude}, Longitude: {record.Longitude}");
        }
        return batch.Count();
    }

    public async Task<int> ProcessAlternateNamesV2BatchAsync(IEnumerable<AlternateNamesV2Record> batch, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} AlternateNameId: {record.AlternateNameId}, GeonameId: {record.GeonameId}, AlternateName: {record.AlternateName}, IsoLanguage: {record.IsoLanguage}, From: {record.From}, To: {record.To}");
        }
        return batch.Count();
    }

    public async Task<int> ProcessTimeZoneBatchAsync(IEnumerable<TimeZoneRecord> batch, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} CountryCode: {record.CountryCode}, TimeZoneId: {record.TimeZoneId}, GMTOffset: {record.GMTOffset}, DSTOffset: {record.DSTOffset}, RawOffset: {record.RawOffset}");
        }
        return batch.Count();
    }

    public async Task<int> ProcessPostalCodeBatchAsync(IEnumerable<PostalCodeRecord> batch, CancellationToken ct = default)
    {
        var rowNumber = 0;
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} CountryCode: {record.CountryCode}, PostalCode: {record.PostalCode}, PlaceName: {record.PlaceName}, Admin1Name: {record.Admin1Name}, Admin1Code: {record.Admin1Code}, Admin2Name: {record.Admin2Name}, Admin2Code: {record.Admin2Code}, Admin3Name: {record.Admin3Name}, Admin3Code: {record.Admin3Code}, Latitude: {record.Latitude}, Longitude: {record.Longitude}, Accuracy: {record.Accuracy}");
        }
        return batch.Count();
    }
}

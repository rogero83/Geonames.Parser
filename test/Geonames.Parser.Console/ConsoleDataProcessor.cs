using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Console;

internal class ConsoleDataProcessor : IDataProcessor
{
    public int rowNumber = 0;
    public async Task<int> ProcessCountryInfoBatchAsync(ICollection<CountryInfoRecord> reader, CancellationToken ct = default)
    {
        foreach (var record in reader)
        {
            System.Console.WriteLine($"{++rowNumber} Country: {record.Country}, Capital: {record.Capital}, Population: {record.Population}");
        }
        return reader.Count;
    }

    public async Task<int> ProcessAdminCodeBatchAsync(AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct = default)
    {
        var i = 0;
        foreach (var record in batch)
        {
            i++;
            if (level == AdminLevel.Admin1)
            {
                if (record is Admin1CodeRecord admin1)
                {
                    System.Console.WriteLine($"{++rowNumber} AdminLevel: {level}, CountryCode: {admin1.CountryCode}, Admin1: {admin1.Admin1Code}, Name: {admin1.Name}, NameAscii: {admin1.NameAscii}, GeonameId: {admin1.GeonameId}");
                }
                else
                {
                    throw new InvalidCastException($"Expected Admin1CodeRecord but got {record.GetType().Name}");
                }
                continue;
            }
            if (level == AdminLevel.Admin2)
            {
                if (record is Admin2CodeRecord admin2)
                {
                    System.Console.WriteLine($"{++rowNumber} AdminLevel: {level}, CountryCode: {admin2.CountryCode}, Admin1: {admin2.Admin1Code}, Admin2: {admin2.Admin2Code}, Name: {admin2.Name}, NameAscii: {admin2.NameAscii}, GeonameId: {admin2.GeonameId}");
                }
                else
                {
                    throw new InvalidCastException($"Expected Admin1CodeRecord but got {record.GetType().Name}");
                }
                continue;
            }
        }
        return i;
    }

    public async Task<int> ProcessGeoNameBatchAsync(ICollection<GeonameRecord> batch, CancellationToken ct = default)
    {
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} GeoNameId: {record.GeonameId}, Name: {record.Name}, CountryCode: {record.CountryCode}, Latitude: {record.Latitude}, Longitude: {record.Longitude}");
        }
        return batch.Count;
    }

    public async Task<int> ProcessAlternateNamesV2BatchAsync(ICollection<AlternateNamesV2Record> batch, CancellationToken ct = default)
    {
        foreach (var record in batch)
        {
            System.Console.WriteLine($"{++rowNumber} AlternateNameId: {record.AlternateNameId}, GeonameId: {record.GeonameId}, AlternateName: {record.AlternateName}, IsoLanguage: {record.IsoLanguage}, From: {record.From}, To: {record.To}");
        }
        return batch.Count;
    }
}

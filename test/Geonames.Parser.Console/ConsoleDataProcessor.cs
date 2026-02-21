using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Console;

internal class ConsoleDataProcessor(int maxBatchSize = 100) : IDataProcessor
{
    private int recordNumber = 0;

    #region CountryInfo Records implementation
    private readonly IList<CountryInfoRecord> countryInfoInternal = [];

    public Task<int> ProcessCountryInfoRecordAsync(CountryInfoRecord record, CancellationToken ct = default)
    {
        countryInfoInternal.Add(record);
        if (countryInfoInternal.Count >= maxBatchSize)
        {
            return Task.FromResult(ProcessCountryInfoBatch(countryInfoInternal));
        }
        return Task.FromResult(0);
    }

    public Task<int> FinalizeCountryInfoRecordAsync(CancellationToken ct = default)
    {
        if (countryInfoInternal.Count > 0)
        {
            var lastRecord = ProcessCountryInfoBatch(countryInfoInternal);
            recordNumber = 0;
            return Task.FromResult(lastRecord);
        }
        return Task.FromResult(0);
    }

    private int ProcessCountryInfoBatch(IEnumerable<CountryInfoRecord> batch)
    {
        var recordProcessed = 0;
        foreach (var item in batch.Where(x => x is not null))
        {
            recordProcessed++;
            System.Console.WriteLine($"{++recordNumber} Country: {item.Country}, Capital: {item.Capital}, Population: {item.Population}");
        }

        countryInfoInternal.Clear();
        return recordProcessed;
    }
    #endregion CountryInfo Records implementation

    public Task<int> ProcessAdmin1CodeRecordAsync(Admin1CodeRecord record, CancellationToken ct = default)
    {
        System.Console.WriteLine($" CountryCode: {record.CountryCode}, Admin1: {record.Admin1Code}, Name: {record.Name}, NameAscii: {record.NameAscii}, GeonameId: {record.GeonameId}");
        return Task.FromResult(1);
    }

    public Task<int> FinalizeAdmin1CodeRecordAsync(CancellationToken ct = default)
    {
        return Task.FromResult(0);
    }

    public Task<int> ProcessAdmin2CodeRecordAsync(Admin2CodeRecord record, CancellationToken ct = default)
    {
        System.Console.WriteLine($" CountryCode: {record.CountryCode}, Admin1: {record.Admin1Code}, Admin2: {record.Admin2Code}, Name: {record.Name}, NameAscii: {record.NameAscii}, GeonameId: {record.GeonameId}");
        return Task.FromResult(1);
    }

    public Task<int> FinalizeAdmin2CodeRecordAsync(CancellationToken ct = default)
    {
        return Task.FromResult(0);
    }

    public Task<int> ProcessGeoNameRecordAsync(GeonameRecord record, CancellationToken ct = default)
    {
        System.Console.WriteLine($" GeoNameId: {record.GeonameId}, Name: {record.Name}, CountryCode: {record.CountryCode}, Latitude: {record.Latitude}, Longitude: {record.Longitude}");
        return Task.FromResult(1);
    }

    public Task<int> FinalizeGeoNameRecordAsync(CancellationToken ct = default)
    {
        return Task.FromResult(0);
    }

    public Task<int> ProcessAlternateNamesV2RecordAsync(AlternateNamesV2Record record, CancellationToken ct = default)
    {
        System.Console.WriteLine($" AlternateNameId: {record.AlternateNameId}, GeonameId: {record.GeonameId}, AlternateName: {record.AlternateName}, IsoLanguage: {record.IsoLanguage}, From: {record.From}, To: {record.To}");
        return Task.FromResult(1);
    }

    public Task<int> FinalizeAlternateNamesV2RecordAsync(CancellationToken ct = default)
    {
        return Task.FromResult(0);
    }

    public Task<int> ProcessTimeZoneRecordAsync(TimeZoneRecord record, CancellationToken ct = default)
    {
        System.Console.WriteLine($" CountryCode: {record.CountryCode}, TimeZoneId: {record.TimeZoneId}, GMTOffset: {record.GMTOffset}, DSTOffset: {record.DSTOffset}, RawOffset: {record.RawOffset}");
        return Task.FromResult(1);
    }

    public Task<int> FinalizeTimeZoneRecordAsync(CancellationToken ct = default)
    {
        return Task.FromResult(0);
    }

    public Task<int> ProcessPostalCodeRecordAsync(PostalCodeRecord record, CancellationToken ct = default)
    {
        System.Console.WriteLine($" CountryCode: {record.CountryCode}, PostalCode: {record.PostalCode}, PlaceName: {record.PlaceName}, Admin1Name: {record.Admin1Name}, Admin1Code: {record.Admin1Code}, Admin2Name: {record.Admin2Name}, Admin2Code: {record.Admin2Code}, Admin3Name: {record.Admin3Name}, Admin3Code: {record.Admin3Code}, Latitude: {record.Latitude}, Longitude: {record.Longitude}, Accuracy: {record.Accuracy}");
        return Task.FromResult(1);
    }

    public Task<int> FinalizePostalCodeRecordAsync(CancellationToken ct = default)
    {
        return Task.FromResult(0);
    }
}

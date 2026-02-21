using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Benchmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class ParserBenchmarks
{
    private class NoOpDataProcessor : IDataProcessor
    {
        public Task<int> ProcessCountryInfoRecordAsync(CountryInfoRecord record, CancellationToken ct = default) => Task.FromResult(1);
        public Task<int> ProcessAdmin1CodeRecordAsync(Admin1CodeRecord record, CancellationToken ct = default) => Task.FromResult(1);
        public Task<int> ProcessAdmin2CodeRecordAsync(Admin2CodeRecord record, CancellationToken ct = default) => Task.FromResult(1);
        public Task<int> ProcessGeoNameRecordAsync(GeonameRecord record, CancellationToken ct = default) => Task.FromResult(1);
        public Task<int> ProcessAlternateNamesV2RecordAsync(AlternateNamesV2Record record, CancellationToken ct = default) => Task.FromResult(1);
        public Task<int> ProcessTimeZoneRecordAsync(TimeZoneRecord record, CancellationToken ct = default) => Task.FromResult(1);
        public Task<int> ProcessPostalCodeRecordAsync(PostalCodeRecord record, CancellationToken ct = default) => Task.FromResult(1);

        public Task<int> FinalizeCountryInfoRecordAsync(CancellationToken ct = default) => Task.FromResult(0);
        public Task<int> FinalizeAdmin1CodeRecordAsync(CancellationToken ct = default) => Task.FromResult(0);
        public Task<int> FinalizeAdmin2CodeRecordAsync(CancellationToken ct = default) => Task.FromResult(0);
        public Task<int> FinalizeGeoNameRecordAsync(CancellationToken ct = default) => Task.FromResult(0);
        public Task<int> FinalizeAlternateNamesV2RecordAsync(CancellationToken ct = default) => Task.FromResult(0);
        public Task<int> FinalizeTimeZoneRecordAsync(CancellationToken ct = default) => Task.FromResult(0);
        public Task<int> FinalizePostalCodeRecordAsync(CancellationToken ct = default) => Task.FromResult(0);
    }

    private GeonamesParser _parser = null!;
    private IDataProcessor _dataProcessor = null!;
    private byte[] _countryInfoContent = null!;
    private byte[] _admin1CodesContent = null!;
    private byte[] _geonamesContent = null!;
    // We'll use a smaller sample for benchmarks to keep them fast but representative enough

    [GlobalSetup]
    public void Setup()
    {
        _dataProcessor = new NoOpDataProcessor();
        _parser = new GeonamesParser();

        // Create some sample data in memory to simulate file reading without disk I/O noise
        _countryInfoContent = System.Text.Encoding.UTF8.GetBytes(
            string.Join("\n", Enumerable.Range(0, 300).Select(i =>
            $"ISO{i}\tISO3{i}\t{i}\tFIPS{i}\tCountry{i}\tCapital{i}\t1000.5\t1000000\tEU\t.tld\tEUR\tEuro\t123456\t###\t^\\d+$\tEn,It\t{i}\t\t")));

        _admin1CodesContent = System.Text.Encoding.UTF8.GetBytes(
            string.Join("\n", Enumerable.Range(0, 1000).Select(i =>
            $"CODE{i}\tName{i}\tNameAscii{i}\t{i}")));

        _geonamesContent = System.Text.Encoding.UTF8.GetBytes(
            string.Join("\n", Enumerable.Range(0, 10000).Select(i =>
            $"3182{i}\tBarletta\tBarletta{i}\tBarletta,baruretta\t{i}.31429\t{i}.28165\tP\tPPLA2\tIT\t\t13\tBT\t1100{i}\t\t93279\t15\t19\tEurope/Rome\t2018-03-16")));
    }

    [Benchmark]
    public async Task ParseCountryInfo()
    {
        using var reader = new MemoryStream(_countryInfoContent);
        await _parser.ParseCountryInfoAsync(reader,
            _dataProcessor.ProcessCountryInfoRecordAsync,
            _dataProcessor.FinalizeCountryInfoRecordAsync);
    }

    [Benchmark]
    public async Task ParseAdmin1Codes()
    {
        using var reader = new MemoryStream(_admin1CodesContent);
        await _parser.ParseAdmin1CodesAsync(reader,
            _dataProcessor.ProcessAdmin1CodeRecordAsync,
            _dataProcessor.FinalizeAdmin1CodeRecordAsync);
    }

    [Benchmark]
    public async Task ParseGeonamesAsync()
    {
        using var reader = new MemoryStream(_geonamesContent);
        await _parser.ParseGeoNamesDataAsync(reader,
            _dataProcessor.ProcessGeoNameRecordAsync,
            _dataProcessor.FinalizeGeoNameRecordAsync);
    }
}

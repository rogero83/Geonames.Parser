using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;

namespace Geonames.Parser.Benchmarks;

//[SimpleJob(RuntimeMoniker.Net80)]
//[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class ParserBenchmarks
{
    private class NoOpDataProcessor : IDataProcessor
    {
        public Task<int> ProcessCountryInfoBatchAsync(IEnumerable<CountryInfoRecord> batch, CancellationToken ct = default) => Task.FromResult(batch.Count());
        public Task<int> ProcessAdminCodeBatchAsync(AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct = default) => Task.FromResult(batch.Count());
        public Task<int> ProcessGeoNameBatchAsync(IEnumerable<GeonameRecord> batch, CancellationToken ct = default) => Task.FromResult(batch.Count());
        public Task<int> ProcessAlternateNamesV2BatchAsync(IEnumerable<AlternateNamesV2Record> batch, CancellationToken ct = default) => Task.FromResult(batch.Count());
        public Task<int> ProcessTimeZoneBatchAsync(IEnumerable<TimeZoneRecord> batch, CancellationToken ct = default) => Task.FromResult(batch.Count());
    }

    private GeonamesParser _parser = null!;
    private string _countryInfoContent = null!;
    private string _admin1CodesContent = null!;
    private string _geonamesContent = null!;
    // We'll use a smaller sample for benchmarks to keep them fast but representative enough

    [GlobalSetup]
    public void Setup()
    {
        _parser = new GeonamesParser(new NoOpDataProcessor());

        // Create some sample data in memory to simulate file reading without disk I/O noise
        _countryInfoContent = string.Join("\n", Enumerable.Range(0, 300).Select(i =>
            $"ISO{i}\tISO3{i}\t{i}\tFIPS{i}\tCountry{i}\tCapital{i}\t1000.5\t1000000\tEU\t.tld\tEUR\tEuro\t123456\t###\t^\\d+$\tEn,It\t{i}\t\t"));

        _admin1CodesContent = string.Join("\n", Enumerable.Range(0, 1000).Select(i =>
            $"CODE{i}\tName{i}\tNameAscii{i}\t{i}"));

        _geonamesContent = string.Join("\n", Enumerable.Range(0, 10000).Select(i =>
            $"3182{i}\tBarletta\tBarletta{i}\tBarletta,baruretta\t{i}.31429\t{i}.28165\tP\tPPLA2\tIT\t\t13\tBT\t1100{i}\t\t93279\t15\t19\tEurope/Rome\t2018-03-16"));
    }

    [Benchmark]
    public async Task ParseCountryInfo()
    {
        using var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_countryInfoContent)));
        await _parser.ParseCountryInfoAsync(reader);
    }

    [Benchmark]
    public async Task ParseAdmin1Codes()
    {
        using var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_admin1CodesContent)));
        await _parser.ParseAdmin1CodesAsync(reader);
    }

    [Benchmark]
    public async Task ParseGeonames()
    {
        using var reader = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_geonamesContent));
        await _parser.ParseGeoNamesDataAsync(reader);
    }
}

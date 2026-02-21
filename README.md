# Geonames Parser

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)

| Package Name | Version | Downloads |
|---|---|---|
| [R83.Geonames.Parser](https://www.nuget.org/packages/R83.Geonames.Parser) | [![NuGet](https://img.shields.io/nuget/v/R83.Geonames.Parser.svg)](https://www.nuget.org/packages/R83.Geonames.Parser/) |[![NuGet](https://img.shields.io/nuget/dt/R83.Geonames.Parser)](https://www.nuget.org/packages/R83.Geonames.Parser) |
| [R83.Geonames.Parser.Contract](https://www.nuget.org/packages/R83.Geonames.Parser.Contract/) | [![NuGet](https://img.shields.io/nuget/v/R83.Geonames.Parser.Contract.svg)](https://www.nuget.org/packages/R83.Geonames.Parser.Contract/) | [![NuGet](https://img.shields.io/nuget/dt/R83.Geonames.Parser.Contract)](https://www.nuget.org/packages/R83.Geonames.Parser.Contract/)

A high-performance .NET library designed to parse data exports from Geonames.org. Version 1.0.0 introduces a modernized, functional-oriented API designed for extreme speed and low memory footprint using `System.IO.Pipelines` and `Span<char>`.

## Key Features

- **Extreme Performance**: Low-allocation parsing using modern .NET features.
- **Functional API**: Process records via delegates for maximum flexibility.
- **Comprehensive Support**: Parses Country Info, Admin Codes, GeoNames records, Alternate Names V2, Time Zones, and Postal Codes.
- **Streaming & Zip Support**: Automatically handles remote ZIP downloads and streams.
- **Filtering**: Built-in support for filtering records during the parsing process.
- **Multi-Framework**: Supports .NET 8.0, 9.0, and 10.0.

## Installation

```bash
dotnet add package R83.Geonames.Parser
```

## Quick Start

The parser is now based on functional delegates, allowing you to process each record as it is parsed.

### 1. Simple Processing with Lambdas

```csharp
using Geonames.Parser;
using Geonames.Parser.Contract.Abstractions;

IGeonamesParser parser = new GeonamesParser();

// Parse data for Italy (IT)
var result = await parser.ParseGeoNamesDataAsync("IT", 
    async (record, ct) => 
    {
        // Process each record here
        Console.WriteLine($"Processing: {record.Name}");
        return 1; // Return 1 for each record processed
    });

Console.WriteLine($"Total processed: {result.RecordsProcessed}");
```

### 2. Using IDataProcessor (Recommended for Complex Projects)

If you prefer a more structured approach, you can still implement `IDataProcessor` and pass its methods as delegates.

```csharp
public class MyDbProcessor : IDataProcessor
{
    public async Task<int> ProcessGeoNameRecordAsync(GeonameRecord record, CancellationToken ct)
    {
        // Save to database
        return 1;
    }

    public async Task<int> FinalizeGeoNameRecordAsync(CancellationToken ct)
    {
        // Commit transaction or cleanup
        return 0;
    }
    
    // ... implement other methods ...
}

// Usage
var processor = new MyDbProcessor();
var result = await parser.ParseGeoNamesDataAsync("US", 
    processor.ProcessGeoNameRecordAsync, 
    processor.FinalizeGeoNameRecordAsync);
```

### 3. Dependency Injection

Register the parser in your `IServiceCollection`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IGeonamesParser, GeonamesParser>();
}
```

## Advanced Filtering

You can optimize processing by providing a filter predicate. Only records that match the filter will be passed to your processor.

```csharp
await parser.ParseGeoNamesDataAsync("US", 
    recordProcessor: myHandler,
    filter: record => record.FeatureClass == "P"); // Only populated places
```

## Performance

The library is optimized for large datasets (like `allCountries.txt`). It leverages:
- **System.IO.Pipelines** for efficient I/O.
- **StringPool** to minimize memory allocations for repeated strings.
- **Span<char>** for parsing without unnecessary string allocations.

## Benchmarks

Run the benchmark project to see performance results on your machine:

```bash
dotnet run -c Release --project test/Geonames.Parser.Benchmarks -f net8.0 net9.0 net10.0
```

## License

This project is licensed under the MIT License.
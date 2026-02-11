# Geonames Parser

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)

| Package Name | Version | Downloads |
|---|---|---|
| [R83.Geonames.Parser](https://www.nuget.org/packages/R83.Geonames.Parser) | [![NuGet](https://img.shields.io/nuget/v/R83.Geonames.Parser.svg)](https://www.nuget.org/packages/R83.Geonames.Parser/) |[![NuGet](https://img.shields.io/nuget/dt/R83.Geonames.Parser)](https://www.nuget.org/packages/R83.Geonames.Parser) |
| [R83.Geonames.Parser.Contract](https://www.nuget.org/packages/R83.Geonames.Parser.Contract/) | [![NuGet](https://img.shields.io/nuget/v/R83.Geonames.Parser.Contract.svg)](https://www.nuget.org/packages/R83.Geonames.Parser.Contract/) | [![NuGet](https://img.shields.io/nuget/dt/R83.Geonames.Parser.Contract)](https://www.nuget.org/packages/R83.Geonames.Parser.Contract/)


A high-performance .NET library designed to parse data exports from Geonames.org. This solution handles the retrieval, decompression, and parsing of Geonames data files, allowing developers to focus on data integration and storage logic.

## Key Features

- **Comprehensive Parsing**: Supports Country Info, Admin Codes (Level 1 & 2), Geonames data, Alternate Names V2, Time Zone.
- **Efficient Processing**: Implements batch processing to handle large datasets efficiently.
- **Filtering**: Built-in support for filtering records (e.g., specific feature codes or countries) during the parsing phase.
- **Async Support**: Fully asynchronous API for non-blocking I/O operations.

## Integration Guide

To use this parser, you must provide an implementation of the data processing logic. This design allows the parser to remain agnostic of the storage medium (SQL, NoSQL, File System, etc.).

### 1. Implement IDataProcessor

Create a class that implements the `Geonames.Parser.Contract.Abstractions.IDataProcessor` interface. This interface defines methods for handling batches of parsed records.

```csharp
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
// ...

public class CustomDataProcessor : IDataProcessor
{
    public async Task<int> ProcessGeoNameBatchAsync(ICollection<GeonameRecord> batch, CancellationToken ct = default)
    {
        // Example: Bulk insert into database
        // await _repository.BulkInsertAsync(batch);
        return batch.Count;
    }

    public async Task<int> ProcessCountryInfoBatchAsync(ICollection<CountryInfoRecord> batch, CancellationToken ct = default)
    {
        // Handle country info
        return batch.Count;
    }

    public async Task<int> ProcessAdmin1CodeBatchAsync(IEnumerable<Admin1CodeRecord> batch, CancellationToken ct = default)
    {
        // Handle admin codes
        return batch.Count();
    }

    public async Task<int> ProcessAdmin2CodeBatchAsync(IEnumerable<Admin2CodeRecord> batch, CancellationToken ct = default)
    {
        // Handle admin codes
        return batch.Count();
    }

    public async Task<int> ProcessAlternateNamesV2BatchAsync(ICollection<AlternateNamesV2Record> batch, CancellationToken ct = default)
    {
        // Handle alternate names
        return batch.Count;
    }

    public async Task<int> ProcessTimeZoneBatchAsync(ICollection<TimeZoneRecord> batch, CancellationToken ct = default)
    {
        // Handle time zones
        return batch.Count;
    }

    public async Task<int> ProcessPostalCodeBatchAsync(IEnumerable<PostalCodeRecord> batch, CancellationToken ct = default)
    {
        // Handle postal codes
        return batch.Count();
    }
}
```

### 2. Configure and Run

You can configure the parser by manually instantiating it or by registering it in your Dependency Injection (DI) container.

#### Option A: Manual Instantiation

```csharp
using Geonames.Parser;

// Initialize your processor
var processor = new CustomDataProcessor();

// Create the parser instance
var parser = new GeonamesParser(processor);

// Example: Parse data for the United States (US)
var result = await parser.ParseGeoNamesDataAsync("US");

Console.WriteLine($"Processed {result.RecordsProcessed} records.");
```

#### Option B: Dependency Injection

If you are using `Microsoft.Extensions.DependencyInjection` (e.g., in ASP.NET Core or a Worker Service), register the services as follows:

```csharp
using Geonames.Parser;
using Geonames.Parser.Contract.Abstractions;
using Microsoft.Extensions.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
    // Register your custom data processor
    services.AddSingleton<IDataProcessor, CustomDataProcessor>();

    // Register the Geonames parser
    services.AddTransient<IGeonamesParser, GeonamesParser>();
}
```

> [!NOTE] The lifetime of `IGeonamesParser` depends on your implementation of `IDataProcessor`. 
> Since `GeonamesParser` itself is stateless (holding only references to the processor and options), it can be registered as a **Singleton** if your `IDataProcessor` is also thread-safe or stateless.
> Adjust the registration (Transient/Scoped/Singleton) to match the lifecycle requirements of your specific integration.

```csharp
// Usage in a service
public class MyService
{
    private readonly IGeonamesParser _parser;

    public MyService(IGeonamesParser parser)
    {
        _parser = parser;
    }

    public async Task RunAsync()
    {
        var result = await _parser.ParseGeoNamesDataAsync("US");
    }
}
```

## Filtering Data

You can pass a filter predicate to process only specific records, reducing unnecessary data handling.

```csharp
var filter = new Func<GeonameRecord, bool>(record =>
{
    // Example: Only parsing populated places (P)
    return record.FeatureClass == GeonamesFeatureClass.P;
});

await parser.ParseGeoNamesDataAsync("US", filter);
```

## Extensibility

The project is designed to be easily extensible. The core logic resides in `Geonames.Parser`, while contracts and models are in `Geonames.Parser.Contract`.

- **Geonames.Parser**: Contains the main `GeonamesParser` class.
- **Geonames.Parser.Contract**: Contains the `IDataProcessor` interface, data models (DTOs), and enums.

To extend the functionality (e.g., adding support for new Geonames files), verify the stream parsing logic in the main parser class and add corresponding models in the contract library.

## Performance Considerations

The project `Geonames.Parser.Benchmarks` contains benchmarks for parsing large datasets. You can run these benchmarks to evaluate performance and identify bottlenecks.

```csharp
dotnet run -c Release -f net8.0 net9.0 net10.0
```
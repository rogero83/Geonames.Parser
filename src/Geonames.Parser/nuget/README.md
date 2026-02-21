# Geonames.Parser

High-performance .NET library for parsing Geonames.org data using System.IO.Pipelines and Span<char> for low memory footprint.
Supports asynchronous processing of Country Info, Admin Codes, GeoNames records, Alternate Names, and more.

## Installation

```bash
dotnet add package R83.Geonames.Parser
```

## Quick Start

Register the parser in your dependency injection container:

```csharp
builder.Services.AddScoped<IGeonamesParser, GeonamesParser>();
```

Then process records using functional delegates:

```csharp
var result = await parser.ParseGeoNamesDataAsync("US", async (record, ct) => 
{
    // Your processing logic
    return 1;
});
```

Read more about the available methods and usage in the [API documentation](https://github.com/rogero83/Geonames.Parser)
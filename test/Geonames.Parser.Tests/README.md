Test Suite for Geonames.Parser

## Overview
This project contains comprehensive unit tests for the `GeonamesParser` class, testing all parsing functionality for GeoNames data including Country Info, Admin Codes, GeoNames data, and Alternate Names V2.

## Test Files

### GeoNamesParserCountryInfoTests.cs
Tests for `ParseCountryInfoAsync` methods:
- Valid content parsing
- Malformed rows with error handling
- Filtering of records
- Comment and empty line skipping
- StreamReader direct parsing
- Custom batch size processing

### GeonamesParserAdmin1CodesTests.cs
Tests for `ParseAdmin1CodesAsync` methods:
- Valid Admin1 code parsing
- Malformed row handling
- Record filtering
- Comment and empty line skipping

### GeonamesParserAdmin2CodesTests.cs
Tests for `ParseAdmin2CodesAsync` methods:
- Valid Admin2 code parsing
- Malformed row handling
- Record filtering

### GeoNamesParserGeoNamesDataTests.cs
Tests for `ParseGeoNamesDataAsync` methods:
- Valid ZIP content parsing
- Missing .txt file error handling
- Malformed rows with error handling
- Record filtering
- Invalid ISO code validation
- "ALL" ISO code (all countries)
- Comment and empty line skipping

### GeonamesParserAlternateNamesV2Tests.cs
Tests for `ParseAlternateNamesV2DataAsync` methods:
- Valid alternate names parsing
- Missing .txt file error handling
- Malformed row handling
- Language filtering
- Invalid ISO code validation
- Boolean flag parsing (preferred, short, historic names)

### GeonamesParserTimeZoneTests.cs
Tests for `ParseTimeZoneDataAsync` methods:
- Valid time zone data parsing
- Malformed row handling
- Filtering by country code
- Malformed rows with error handling
- Comment and empty line skipping
	
## Test Patterns

All tests follow these patterns:

### Arrange - Act - Assert (AAA)
```csharp
// Arrange: Set up test data and mocks
// Act: Execute the method being tested
// Assert: Verify results
```

### Mock Usage
- Uses `Moq` library for mocking `IDataProcessor`
- `TestHttpMessageHandler` provides controlled HTTP responses
- Mock setup uses `ReturnsAsync` for async operations

### Common Test Scenarios
1. **Success Cases**: Valid data is parsed correctly
2. **Failure Cases**: Malformed rows are skipped with error messages
3. **Filtering**: Records matching filter criteria are processed
4. **Edge Cases**: Comments, empty lines, invalid ISO codes handled properly
5. **Batch Processing**: Large datasets split into configured batch sizes

## Running Tests

```bash
dotnet test test/Geonames.Parser.Tests/Geonames.Parser.Tests.csproj
```

## Dependencies

- **Xunit**: Unit testing framework
- **Moq**: Mocking library
- **System.Net.Http**: HTTP client functionality
- **System.IO.Compression**: ZIP archive handling

## Notes

- Tests use `HttpClient` overloads for HTTP mocking to avoid network calls
- `TestHttpMessageHandler` allows tests to provide predefined responses
- All async methods are properly tested with `CancellationToken` support
- Batch size configuration is validated through settings parameter

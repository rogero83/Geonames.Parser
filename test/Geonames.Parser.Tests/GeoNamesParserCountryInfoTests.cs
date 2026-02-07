using Geonames.Parser.Contract;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Tests.Utility;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geonames.Parser.Tests;

public class GeoNamesParserCountryInfoTests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParseCountryInfoAsync_WithValidContent_ReturnsProcessed()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# comment");
        content.AppendLine("US\tUSA\t840\tUS\tUnited States\tWashington\t9629091\t331002651\tNA\t.us\tUSD\tDollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-US,es-MX\t6252001\tCA,MX\t");
        content.AppendLine("CA\tCAN\t124\tCA\tCanada\tOttawa\t9629091\t38005238\tNA\t.ca\tCAD\tCanadian Dollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-CA,fr-CA\t6251999\tUS\t");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessCountryInfoBatchAsync(It.IsAny<ICollection<CountryInfoRecord>>(), ct))
            .ReturnsAsync((ICollection<CountryInfoRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseCountryInfoAsync(client, null, ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseCountryInfoAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# header");
        content.AppendLine("US\tUSA\t840\tUS\tUnited States\tWashington\t9629091\t331002651\tNA\t.us\tUSD\tDollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-US,es-MX\t6252001\tCA,MX\t");
        content.AppendLine("MALFORMED_LINE_WITH_FEW_FIELDS");
        content.AppendLine("CA\tCAN\t124\tCA\tCanada\tOttawa\t9629091\t38005238\tNA\t.ca\tCAD\tCanadian Dollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-CA,fr-CA\t6251999\tUS\t");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessCountryInfoBatchAsync(It.IsAny<IEnumerable<CountryInfoRecord>>(), ct))
            .ReturnsAsync((IEnumerable<CountryInfoRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseCountryInfoAsync(client, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseCountryInfoAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("US\tUSA\t840\tUS\tUnited States\tWashington\t9629091\t331002651\tNA\t.us\tUSD\tDollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-US,es-MX\t6252001\tCA,MX\t");
        content.AppendLine("CA\tCAN\t124\tCA\tCanada\tOttawa\t9629091\t38005238\tNA\t.ca\tCAD\tCanadian Dollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-CA,fr-CA\t6251999\tUS\t");
        content.AppendLine("MX\tMEX\t484\tMX\tMexico\tMexico City\t9629091\t128932753\tNA\t.mx\tMXN\tMexican Peso\t52\t#####\t\\d{5}\tes-MX\t3996063\tUS,GT,BZ\t");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessCountryInfoBatchAsync(It.IsAny<IEnumerable<CountryInfoRecord>>(), ct))
            .ReturnsAsync((IEnumerable<CountryInfoRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act - only include NA continent
        var result = await parser.ParseCountryInfoAsync(client, r => r.Continent == "NA", ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(3, result.RecordsProcessed);
        Assert.Equal(3, result.RecordsAdded);
    }

    [Fact]
    public async Task ParseCountryInfoAsync_WithCommentAndEmptyLines_SkipsThem()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# This is a comment");
        content.AppendLine("");
        content.AppendLine("US\tUSA\t840\tUS\tUnited States\tWashington\t9629091\t331002651\tNA\t.us\tUSD\tDollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-US,es-MX\t6252001\tCA,MX\t");
        content.AppendLine("   ");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessCountryInfoBatchAsync(It.IsAny<ICollection<CountryInfoRecord>>(), ct))
            .ReturnsAsync((IEnumerable<CountryInfoRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseCountryInfoAsync(client, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseCountryInfoAsync_WithStreamReader_ParsesCorrectly()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("US\tUSA\t840\tUS\tUnited States\tWashington\t9629091\t331002651\tNA\t.us\tUSD\tDollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-US,es-MX\t6252001\tCA,MX\t");

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));
        using var reader = new StreamReader(stream);

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessCountryInfoBatchAsync(It.IsAny<ICollection<CountryInfoRecord>>(), ct))
            .ReturnsAsync((IEnumerable<CountryInfoRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseCountryInfoAsync(reader, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseCountryInfoAsync_WithCustomBatchSize_ProcessesCorrectly()
    {
        // Arrange
        var settings = new GeonamesParserOptions { ProcessingBatchSize = 2 };
        var content = new StringBuilder();
        content.AppendLine("US\tUSA\t840\tUS\tUnited States\tWashington\t9629091\t331002651\tNA\t.us\tUSD\tDollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-US,es-MX\t6252001\tCA,MX\t");
        content.AppendLine("CA\tCAN\t124\tCA\tCanada\tOttawa\t9629091\t38005238\tNA\t.ca\tCAD\tCanadian Dollar\t1\t#####\t\\d{5}(-\\d{4})?\ten-CA,fr-CA\t6251999\tUS\t");
        content.AppendLine("MX\tMEX\t484\tMX\tMexico\tMexico City\t9629091\t128932753\tNA\t.mx\tMXN\tMexican Peso\t52\t#####\t\\d{5}\tes-MX\t3996063\tUS,GT,BZ\t");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var batchCallCount = 0;
        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessCountryInfoBatchAsync(It.IsAny<IEnumerable<CountryInfoRecord>>(), ct))
            .Callback((IEnumerable<CountryInfoRecord> reader, CancellationToken ct) => batchCallCount++)
            .ReturnsAsync((IEnumerable<CountryInfoRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object, settings);

        // Act
        var result = await parser.ParseCountryInfoAsync(client, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(3, result.RecordsProcessed);
        Assert.Equal(2, batchCallCount); // Should be called twice (2 items, then 1 item)
    }
}

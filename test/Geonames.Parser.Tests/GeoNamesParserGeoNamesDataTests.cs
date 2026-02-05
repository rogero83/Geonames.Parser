using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Tests.Utility;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geonames.Parser.Tests;

public class GeoNamesParserGeoNamesDataTests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithValidZipContent_ReturnsProcessed()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("# comment");
        content.AppendLine("123\tNew York\tNew York\tAlt\t40.7128\t-74.0060\tP\tPPL\tUS\t\tNY\t\t\t\t8000000\t10\t5\tAmerica/New_York\t2020-01-01");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessGeoNameBatchAsync(It.IsAny<List<GeonameRecord>>(), ct))
            .ReturnsAsync((List<GeonameRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseGeoNamesDataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithMissingTxtEntry_ReturnsError()
    {
        // Arrange
        var isoCode = "ZZ";
        using var ms = new MemoryStream();
        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("other.txt");
            using var entryStream = entry.Open();
            using var sw = new StreamWriter(entryStream, Encoding.UTF8);
            sw.Write("content");
        }
        ms.Position = 0;

        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseGeoNamesDataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(0, result.RecordsProcessed);
        Assert.Equal(0, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("No .txt file found", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("123\tNew York\tNew York\tAlt\t40.7128\t-74.0060\tP\tPPL\tUS\t\tNY\t\t\t\t8000000\t10\t5\tAmerica/New_York\t2020-01-01");
        content.AppendLine("MALFORMED");
        content.AppendLine("124\tBoston\tBoston\tAlt\t42.3601\t-71.0589\tP\tPPL\tUS\t\tMA\t\t\t\t\t\t\tAmerica/New_York\t2020-01-02");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessGeoNameBatchAsync(It.IsAny<List<GeonameRecord>>(), ct))
            .ReturnsAsync((List<GeonameRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseGeoNamesDataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("123\tNew York\tNew York\tAlt\t40.7128\t-74.0060\tP\tPPL\tUS\t\tNY\t\t\t\t8000000\t\t\tAmerica/New_York\t2020-01-01");
        content.AppendLine("124\tBoston\tBoston\tAlt\t42.3601\t-71.0589\tP\tPPL\tUS\t\tMA\t\t\t\t\t\t\tAmerica/New_York\t2020-01-02");
        content.AppendLine("125\tPhiladelphia\tPhiladelphia\tAlt\t39.9526\t-75.1652\tP\tPPL\tUS\t\tPA\t\t\t\t\t\t\tAmerica/New_York\t2020-01-03");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessGeoNameBatchAsync(It.IsAny<List<GeonameRecord>>(), ct))
            .ReturnsAsync((List<GeonameRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act - only include NY records
        var result = await parser.ParseGeoNamesDataAsync(client, isoCode, r => r.Admin1Code == "NY", ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithInvalidIsoCode_ReturnsError()
    {
        // Arrange
        var mockProcessor = new Mock<IDataProcessor>();
        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseGeoNamesDataAsync((HttpClient)null, "INVALID", null, ct);

        // Assert
        Assert.Equal(0, result.RecordsProcessed);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Invalid ISO code", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithAllIsoCode_ProcessesAllCountries()
    {
        // Arrange
        var isoCode = "ALL";
        var content = new StringBuilder();
        content.AppendLine("123\tNew York\tNew York\tAlt\t40.7128\t-74.0060\tP\tPPL\tUS\t\tNY\t\t\t\t\t\t\tAmerica/New_York\t2020-01-01");
        content.AppendLine("456\tToronto\tToronto\tAlt\t43.6532\t-79.3832\tP\tPPL\tCA\t\tON\t\t\t\t\t\t\tAmerica/Toronto\t2020-01-01");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessGeoNameBatchAsync(It.IsAny<List<GeonameRecord>>(), ct))
            .ReturnsAsync((List<GeonameRecord> b, System.Threading.CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseGeoNamesDataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseGeoNamesDataAsync_WithCommentAndEmptyLines_SkipsThem()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("# This is a comment");
        content.AppendLine("");
        content.AppendLine("123\tNew York\tNew York\tAlt\t40.7128\t-74.0060\tP\tPPL\tUS\t\tNY\t\t\t\t\t\t\tAmerica/New_York\t2020-01-01");
        content.AppendLine("   ");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessGeoNameBatchAsync(It.IsAny<List<GeonameRecord>>(), ct))
            .ReturnsAsync((List<GeonameRecord> b, System.Threading.CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseGeoNamesDataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }
}

using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Tests.Utility;
using Moq;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geonames.Parser.Tests;

public class GeonamesParserPostalCodeDataTests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithValidZipContent_ReturnsProcessed()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("US\t90210\tBeverly Hills\tCalifornia\tCA\tLos Angeles\t037\t\t\t34.0901\t-118.4065\t4");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessPostalCodeRecordAsync(It.IsAny<PostalCodeRecord>(), ct))
            .ReturnsAsync((PostalCodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParsePostalCodeDataAsync(client, isoCode, true,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithFullFlag_AppendsFullToIsoCode()
    {
        // Arrange
        var isoCode = "GB"; // GB has full postal codes
        var expectedIsoInZip = "GB_full";
        var content = new StringBuilder();
        content.AppendLine("GB\tSW1A 1AA\tLondon\tEngland\tENG\tGreater London\t\t\t\t51.501\t-0.142\t4");

        using var ms = ZipUtility.CreateZipWithContent(expectedIsoInZip, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessPostalCodeRecordAsync(It.IsAny<PostalCodeRecord>(), ct))
            .ReturnsAsync((PostalCodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParsePostalCodeDataAsync(client, isoCode, true,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null,
            null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithMissingTxtEntry_ReturnsError()
    {
        // Arrange
        var isoCode = "IT";
        using var ms = new MemoryStream();
        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry("wrong_name.txt");
            using var entryStream = entry.Open();
            using var sw = new StreamWriter(entryStream, Encoding.UTF8);
            sw.Write("content");
        }
        ms.Position = 0;

        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParsePostalCodeDataAsync(client, isoCode, true,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null,
            null, ct);

        // Assert
        Assert.Equal(0, result.RecordsProcessed);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("No .txt file found", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("US\t90210\tBeverly Hills\tCalifornia\tCA\tLos Angeles\t037\t\t\t34.0901\t-118.4065\t4");
        content.AppendLine("MALFORMED");
        content.AppendLine("US\t10001\tNew York\tNew York\tNY\tNew York\t061\t\t\t40.7501\t-73.9961\t4");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessPostalCodeRecordAsync(It.IsAny<PostalCodeRecord>(), ct))
            .ReturnsAsync((PostalCodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParsePostalCodeDataAsync(client, isoCode, true,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null,
            null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("US\t90210\tBeverly Hills\tCalifornia\tCA\tLos Angeles\t037\t\t\t34.0901\t-118.4065\t4");
        content.AppendLine("US\t10001\tNew York\tNew York\tNY\tNew York\t061\t\t\t40.7501\t-73.9961\t4");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessPostalCodeRecordAsync(It.IsAny<PostalCodeRecord>(), ct))
            .ReturnsAsync((PostalCodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act - only Beverly Hills
        var result = await parser.ParsePostalCodeDataAsync(client, isoCode, true,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null,
            r => r.PostalCode == "90210", ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
    }

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithInvalidIsoCode_ReturnsError()
    {
        // Arrange
        var mockProcessor = new Mock<IDataProcessor>();
        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParsePostalCodeDataAsync(new HttpClient(), "INVALID", true,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null,
            null, ct);

        // Assert
        Assert.Equal(0, result.RecordsProcessed);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Invalid ISO code", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParsePostalCodeDataAsync_WithStream_ReturnsProcessed()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("US\t90210\tBeverly Hills\tCalifornia\tCA\tLos Angeles\t037\t\t\t34.0901\t-118.4065\t4");
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessPostalCodeRecordAsync(It.IsAny<PostalCodeRecord>(), ct))
            .ReturnsAsync((PostalCodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParsePostalCodeDataAsync(stream,
            mockProcessor.Object.ProcessPostalCodeRecordAsync,
            null,
            null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }
}

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

public class GeonamesParserAlternateNamesV2Tests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParseAlternateNamesV2DataAsync_WithValidContent_ReturnsProcessed()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("# comment");
        content.AppendLine("1\t123\ten\tAlternate Name\t0\t0\t0\t0\t\t");
        content.AppendLine("2\t124\tfr\tNom Alternatif\t0\t0\t0\t0\t\t");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAlternateNamesV2BatchAsync(It.IsAny<List<AlternateNamesV2Record>>(), ct))
            .ReturnsAsync((List<AlternateNamesV2Record> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAlternateNamesV2DataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseAlternateNamesV2DataAsync_WithMissingTxtEntry_ReturnsError()
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
        var result = await parser.ParseAlternateNamesV2DataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(0, result.RecordsProcessed);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("No .txt file found", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseAlternateNamesV2DataAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("1\t123\ten\tAlternate Name\t0\t0\t0\t0\t\t");
        content.AppendLine("MALFORMED");
        content.AppendLine("2\t124\tfr\tNom Alternatif\t0\t0\t0\t0\t\t");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAlternateNamesV2BatchAsync(It.IsAny<List<AlternateNamesV2Record>>(), ct))
            .ReturnsAsync((List<AlternateNamesV2Record> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAlternateNamesV2DataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseAlternateNamesV2DataAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("1\t123\ten\tEnglish Name\t0\t0\t0\t0\t\t");
        content.AppendLine("2\t124\tfr\tNom Francais\t0\t0\t0\t0\t\t");
        content.AppendLine("3\t125\tde\tDeutcher Name\t0\t0\t0\t0\t\t");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAlternateNamesV2BatchAsync(It.IsAny<List<AlternateNamesV2Record>>(), ct))
            .ReturnsAsync((List<AlternateNamesV2Record> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act - only include English names
        var result = await parser.ParseAlternateNamesV2DataAsync(client, isoCode, r => r.IsoLanguage == "en", ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseAlternateNamesV2DataAsync_WithInvalidIsoCode_ReturnsError()
    {
        // Arrange
        var mockProcessor = new Mock<IDataProcessor>();
        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAlternateNamesV2DataAsync((HttpClient)null, "INVALID", null, ct);

        // Assert
        Assert.Equal(0, result.RecordsProcessed);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Invalid ISO code", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseAlternateNamesV2DataAsync_WithPreferredAndShortFlags_ParsesCorrectly()
    {
        // Arrange
        var isoCode = "US";
        var content = new StringBuilder();
        content.AppendLine("1\t123\ten\tMain Name\t1\t0\t0\t0\t\t");
        content.AppendLine("2\t124\tfr\tShort\t0\t1\t0\t0\t\t");
        content.AppendLine("3\t125\tde\tHistoric\t0\t0\t0\t1\t\t");

        using var ms = ZipUtility.CreateZipWithContent(isoCode, content.ToString());
        var httpContent = new StreamContent(ms);
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        var capturedBatch = new List<AlternateNamesV2Record>();
        mockProcessor.Setup(x => x.ProcessAlternateNamesV2BatchAsync(It.IsAny<IEnumerable<AlternateNamesV2Record>>(), ct))
            .Callback((IEnumerable<AlternateNamesV2Record> b, CancellationToken ct) => capturedBatch.AddRange(b))
            .ReturnsAsync((List<AlternateNamesV2Record> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAlternateNamesV2DataAsync(client, isoCode, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(3, result.RecordsProcessed);
        Assert.True(capturedBatch[0].IsPreferredName);
        Assert.True(capturedBatch[1].IsShortName);
        Assert.True(capturedBatch[2].IsHistoric);
    }
}

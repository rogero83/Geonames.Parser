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

public class GeonamesParserTimeZoneTests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParseTimeZoneDataAsync_WithValidContent_ReturnsProcessed()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("AD\tEurope/Andorra\t1.0\t2.0\t1.0");
        content.AppendLine("AE\tAsia/Dubai\t4.0\t4.0\t4.0");

        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessTimeZoneBatchAsync(It.IsAny<IEnumerable<TimeZoneRecord>>(), ct))
            .ReturnsAsync((IEnumerable<TimeZoneRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseTimeZoneDataAsync(ms, null, ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseTimeZoneDataAsync_WithHttpClient_ReturnsProcessed()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("CountryCode\tTimeZoneId\tGMT offset 1. Jan 2026\tDST offset 1. Jul 2026\traw offset (independant of DST)");
        content.AppendLine("AD\tEurope/Andorra\t1.0\t2.0\t1.0");

        var httpContent = new StringContent(content.ToString());
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessTimeZoneBatchAsync(It.IsAny<IEnumerable<TimeZoneRecord>>(), ct))
            .ReturnsAsync((IEnumerable<TimeZoneRecord> b, CancellationToken ct) => b.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseTimeZoneDataAsync(client, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseTimeZoneDataAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("AD\tEurope/Andorra\t1.0\t2.0\t1.0");
        content.AppendLine("AE\tAsia/Dubai\t4.0\t4.0\t4.0");

        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessTimeZoneBatchAsync(It.IsAny<List<TimeZoneRecord>>(), ct))
            .ReturnsAsync((List<TimeZoneRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act - only include AE records
        var result = await parser.ParseTimeZoneDataAsync(ms, r => r.CountryCode == "AE", ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseTimeZoneDataAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("AD\tEurope/Andorra\t1.0\t2.0\t1.0");
        content.AppendLine("MALFORMED");
        content.AppendLine("AE\tAsia/Dubai\t4.0\t4.0\t4.0");

        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessTimeZoneBatchAsync(It.IsAny<List<TimeZoneRecord>>(), ct))
            .ReturnsAsync((List<TimeZoneRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseTimeZoneDataAsync(ms, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseTimeZoneDataAsync_WithCommentAndHeaderLines_SkipsThem()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# This is a comment");
        content.AppendLine("CountryCode\tTimeZoneId\tGMT offset 1. Jan 2026\tDST offset 1. Jul 2026\traw offset (independant of DST)");
        content.AppendLine("");
        content.AppendLine("AD\tEurope/Andorra\t1.0\t2.0\t1.0");

        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(content.ToString()));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessTimeZoneBatchAsync(It.IsAny<List<TimeZoneRecord>>(), ct))
            .ReturnsAsync((List<TimeZoneRecord> b, CancellationToken ct) => b.Count);

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseTimeZoneDataAsync(ms, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }
}

using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Tests.Utility;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geonames.Parser.Tests;

public class GeonamesParserAdmin1CodesTests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParseAdmin1CodesAsync_WithValidContent_ReturnsProcessed()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# Admin1 codes");
        content.AppendLine("US.CA\tCalifornia\tCalifornia\t5332921");
        content.AppendLine("US.TX\tTexas\tTexas\t4736286");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdminCodeBatchAsync(It.IsAny<AdminLevel>(), It.IsAny<IEnumerable<AdminXCodeRecord>>(), ct))
            .ReturnsAsync((AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct) => batch.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAdmin1CodesAsync(client, null, ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseAdmin1CodesAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# Admin1 codes");
        content.AppendLine("US.CA\tCalifornia\tCalifornia\t5332921");
        content.AppendLine("MALFORMED");
        content.AppendLine("US.TX\tTexas\tTexas\t4736286");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdminCodeBatchAsync(
                It.IsAny<AdminLevel>(),
                It.IsAny<IEnumerable<AdminXCodeRecord>>(), ct))
            .ReturnsAsync((AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct)
            => batch.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAdmin1CodesAsync(client, null, ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseAdmin1CodesAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("US.CA\tCalifornia\tCalifornia\t5332921");
        content.AppendLine("US.TX\tTexas\tTexas\t4736286");
        content.AppendLine("US.NY\tNew York\tNew York\t5128581");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdminCodeBatchAsync(It.IsAny<AdminLevel>(), It.IsAny<IEnumerable<AdminXCodeRecord>>(), ct))
            .ReturnsAsync((AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct) => batch.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act - only include CA records
        var result = await parser.ParseAdmin1CodesAsync(client, r => r.Code.EndsWith("CA"), ct);

        // Assert
        Assert.Equal(3, result.RecordsFound); // totalRecords = 3
        Assert.Equal(1, result.RecordsProcessed); // only CA matched filter
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseAdmin1CodesAsync_WithCommentAndEmptyLines_SkipsThem()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# This is a comment");
        content.AppendLine("");
        content.AppendLine("US.CA\tCalifornia\tCalifornia\t5332921");
        content.AppendLine("   ");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdminCodeBatchAsync(It.IsAny<AdminLevel>(), It.IsAny<IEnumerable<AdminXCodeRecord>>(), TestContext.Current.CancellationToken))
            .ReturnsAsync((AdminLevel level, IEnumerable<AdminXCodeRecord> batch, CancellationToken ct) => batch.Count());

        var parser = new GeonamesParser(mockProcessor.Object);

        // Act
        var result = await parser.ParseAdmin1CodesAsync(client, null, ct);

        // Assert
        Assert.Equal(1, result.RecordsFound);
        Assert.Equal(1, result.RecordsProcessed);
        Assert.Equal(1, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }
}

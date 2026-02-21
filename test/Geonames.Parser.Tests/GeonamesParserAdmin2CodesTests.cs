using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Tests.Utility;
using Moq;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Geonames.Parser.Tests;

public class GeonamesParserAdmin2CodesTests
{
    private readonly CancellationToken ct = TestContext.Current.CancellationToken;

    [Fact]
    public async Task ParseAdmin2CodesAsync_WithValidContent_ReturnsProcessed()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("# Admin2 codes");
        content.AppendLine("US.CA.001\tAlameda\tAlameda\t5323");
        content.AppendLine("US.CA.003\tAlpine\tAlpine\t5326");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdmin2CodeRecordAsync(It.IsAny<Admin2CodeRecord>(), ct))
            .ReturnsAsync((Admin2CodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParseAdmin2CodesAsync(client,
            mockProcessor.Object.ProcessAdmin2CodeRecordAsync,
            mockProcessor.Object.FinalizeAdmin2CodeRecordAsync,
            null, ct);

        // Assert
        Assert.Equal(2, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }

    [Fact]
    public async Task ParseAdmin2CodesAsync_WithMalformedRow_AddsErrorAndContinues()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("US.CA.001\tAlameda\tAlameda\t5323");
        content.AppendLine("MALFORMED");
        content.AppendLine("US.CA.003\tAlpine\tAlpine\t5326");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdmin2CodeRecordAsync(It.IsAny<Admin2CodeRecord>(), ct))
            .ReturnsAsync((Admin2CodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act
        var result = await parser.ParseAdmin2CodesAsync(client,
            mockProcessor.Object.ProcessAdmin2CodeRecordAsync,
            mockProcessor.Object.FinalizeAdmin2CodeRecordAsync,
            ct: ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Single(result.ErrorMessages);
        Assert.Contains("Skipping malformed row", result.ErrorMessages.First());
    }

    [Fact]
    public async Task ParseAdmin2CodesAsync_WithFilter_OnlyProcessesMatchingRecords()
    {
        // Arrange
        var content = new StringBuilder();
        content.AppendLine("US.CA.001\tAlameda\tAlameda\t5323");
        content.AppendLine("US.TX.001\tAnderson\tAnderson\t4671654");
        content.AppendLine("US.CA.003\tAlpine\tAlpine\t5326");

        var httpContent = new StringContent(content.ToString(), Encoding.UTF8, "text/plain");
        using var client = new HttpClient(new TestHttpMessageHandler(httpContent));

        var mockProcessor = new Mock<IDataProcessor>();
        mockProcessor.Setup(x => x.ProcessAdmin2CodeRecordAsync(It.IsAny<Admin2CodeRecord>(), ct))
            .ReturnsAsync((Admin2CodeRecord b, CancellationToken ct) => 1);

        var parser = new GeonamesParser();

        // Act - only include CA codes
        var result = await parser.ParseAdmin2CodesAsync(client,
            mockProcessor.Object.ProcessAdmin2CodeRecordAsync,
            mockProcessor.Object.FinalizeAdmin2CodeRecordAsync,
            r => r.Code.Contains("US.CA"),
            ct);

        // Assert
        Assert.Equal(3, result.RecordsFound);
        Assert.Equal(2, result.RecordsProcessed);
        Assert.Equal(2, result.RecordsAdded);
        Assert.Empty(result.ErrorMessages);
    }
}

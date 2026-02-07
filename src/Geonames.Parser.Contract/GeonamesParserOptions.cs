namespace Geonames.Parser.Contract;

/// <summary>
/// Options for GeonamesParser
/// </summary>
public class GeonamesParserOptions
{
    /// <summary>
    /// Default options for GeonamesParser
    /// </summary>
    public static GeonamesParserOptions Default => new();

    private int _processingBatchSize;
    private const int DefaultProcessingBatchSize = 1000;

    /// <summary>
    /// Dimensione del batch per l'elaborazione dei record
    /// </summary>
    public int ProcessingBatchSize
    {
        get => _processingBatchSize;
        init => _processingBatchSize = value > 0 ? value : DefaultProcessingBatchSize;
    }
}

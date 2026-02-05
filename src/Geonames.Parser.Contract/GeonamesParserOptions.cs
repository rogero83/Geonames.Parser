namespace Geonames.Parser.Contract;

public class GeonamesParserOptions
{
    public static GeonamesParserOptions Default => new();

    private int _defaultProcessingBatchSize;
    private const int DefaultProcessingBatchSize = 1000;

    /// <summary>
    /// Dimensione del batch per l'elaborazione dei record
    /// </summary>
    public int ProcessingBatchSize
    {
        get => _defaultProcessingBatchSize;
        init => _defaultProcessingBatchSize = value > 0 ? value : DefaultProcessingBatchSize;
    }
}

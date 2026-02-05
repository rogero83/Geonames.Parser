namespace Geonames.Parser.Contract.Models;

public class ParserResult
{
    /// <summary>
    /// Total RecordFound excluded comments
    /// </summary>
    public int RecordsFound { get; set; }
    /// <summary>
    /// Record successfully processed with option filtres applied
    /// </summary>
    public int RecordsProcessed { get; set; }
    /// <summary>
    /// Record added to the target storage
    /// </summary>
    public int RecordsAdded { get; set; }
    /// <summary>
    /// List of error messages encountered during parsing
    /// </summary>
    public ICollection<string> ErrorMessages { get; set; } = [];

    public static ParserResult Error(string errorMessage) => new()
    {
        ErrorMessages = [errorMessage]
    };

    public static ParserResult Errors(string[] errorMessage) => new()
    {
        ErrorMessages = errorMessage
    };
}

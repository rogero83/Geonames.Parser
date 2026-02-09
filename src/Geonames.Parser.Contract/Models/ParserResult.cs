namespace Geonames.Parser.Contract.Models;

/// <summary>
/// Represents the result of a parsing operation, including counts of records processed and any error messages
/// encountered.
/// </summary>
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
    /// Total record in file, included comments and excluded by filters, useful for logging and debugging purposes
    /// </summary>
    public int RecordsTotal { get; set; }
    /// <summary>
    /// List of error messages encountered during parsing
    /// </summary>
    public ICollection<string> ErrorMessages { get; set; } = [];

    /// <summary>
    /// Creates a parser result representing an error with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message describing the parsing failure. Cannot be null or empty.</param>
    /// <returns>A ParserResult instance containing the provided error message.</returns>
    public static ParserResult Error(string errorMessage) => new()
    {
        ErrorMessages = [errorMessage]
    };

    /// <summary>
    /// Creates a parser result representing an error with the specified error messages.
    /// </summary>
    /// <param name="errorMessage">The error messages describing the parsing failure. Cannot be null or empty.</param>
    /// <returns>A ParserResult instance containing the provided error message.</returns>
    public static ParserResult Errors(string[] errorMessage) => new()
    {
        ErrorMessages = errorMessage
    };
}

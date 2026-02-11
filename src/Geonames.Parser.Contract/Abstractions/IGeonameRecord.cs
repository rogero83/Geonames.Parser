namespace Geonames.Parser.Contract.Abstractions;

/// <summary>
/// Geoname record interface, implemented by all records that represent a geoname.
/// </summary>
public interface IGeonameRecord
{
    /// <summary>
    /// Number of fields in the record, used for validation purposes.
    /// </summary>
    /// <returns></returns>
    static abstract int NumberOfFields { get; }
}
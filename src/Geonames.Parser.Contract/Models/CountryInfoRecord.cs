namespace Geonames.Parser.Contract.Models;

/// <summary>
/// Represents a single record from a CountryInfo data file.
/// see: https://download.geonames.org/export/dump/countryInfo.txt
/// </summary>
public class CountryInfoRecord
{
    /// <summary>
    /// Coutry ISO code
    /// </summary>
    /// <remarks>The ISO code typically follows international standards, such as ISO 3166 for country codes or
    /// ISO 639 for language codes. The expected format and meaning depend on the context in which this property is
    /// used.</remarks>
    public string ISO { get; set; } = null!;
    public string ISO3 { get; set; } = string.Empty;

    /// <summary>
    /// ISO-Numeric
    /// </summary>
    public string ISO_Numeric { get; set; } = string.Empty;
    /// <summary>
    /// Federal Information Processing Standards (FIPS) code associated with the entity.
    /// </summary>
    public string Fips { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Capital { get; set; } = string.Empty;
    public string Population { get; set; } = string.Empty;
    public string Continent { get; set; } = string.Empty;
    /// <summary>
    /// Top-level domain (TLD) associated with the entity.
    /// </summary>
    public string Tld { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string CurrencyName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Postal_Code_Format { get; set; } = string.Empty;
    public string Postal_Code_Regex { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty;
    public string GeonameId { get; set; } = null!;
    public string Neighbours { get; set; } = string.Empty;
    public string EquivalentFipsCode { get; set; } = string.Empty;
}

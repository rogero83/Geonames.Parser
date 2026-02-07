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
    /// <summary>
    /// Coutry ISO code 3 letters
    /// </summary>
    public string ISO3 { get; set; } = string.Empty;
    /// <summary>
    /// ISO-Numeric
    /// </summary>
    public string ISO_Numeric { get; set; } = string.Empty;
    /// <summary>
    /// Federal Information Processing Standards (FIPS) code associated with the entity.
    /// </summary>
    public string Fips { get; set; } = string.Empty;
    /// <summary>
    /// Country name in English.
    /// </summary>
    public string Country { get; set; } = string.Empty;
    /// <summary>
    /// Capital
    /// </summary>
    public string Capital { get; set; } = string.Empty;
    /// <summary>
    /// Area(in sq km)
    /// </summary>
    public decimal Area { get; set; }
    /// <summary>
    /// Population
    /// </summary>
    public long Population { get; set; }
    /// <summary>
    /// Continent<br/>
    /// AF : Africa			    geonameId = 6255146<br/>
    /// AS : Asia               geonameId = 6255147<br/>
    /// EU : Europe             geonameId = 6255148<br/>
    /// NA : North America      geonameId = 6255149<br/>
    /// OC : Oceania            geonameId = 6255151<br/>
    /// SA : South America      geonameId = 6255150<br/>
    /// AN : Antarctica         geonameId = 6255152<br/>
    /// </summary>
    public string Continent { get; set; } = string.Empty;
    /// <summary>
    /// Top-level domain (TLD) associated with the entity.
    /// </summary>
    public string Tld { get; set; } = string.Empty;
    /// <summary>
    /// CurrencyCode
    /// </summary>
    public string CurrencyCode { get; set; } = string.Empty;
    /// <summary>
    /// CurrencyName
    /// </summary>
    public string CurrencyName { get; set; } = string.Empty;
    /// <summary>
    /// Phone prefix
    /// </summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>
    /// Postal Code Format
    /// </summary>
    public string Postal_Code_Format { get; set; } = string.Empty;
    /// <summary>
    /// Postal Code Regex
    /// </summary>
    public string Postal_Code_Regex { get; set; } = string.Empty;
    /// <summary>
    /// List of languages separated with comma.
    /// </summary>
    public string Languages { get; set; } = string.Empty;
    /// <summary>
    /// GeoNameId
    /// </summary>
    public int GeonameId { get; set; }
    /// <summary>
    /// Borders in ISO separated with comma.
    /// </summary>
    public string Neighbours { get; set; } = string.Empty;
    /// <summary>
    /// The Federal Information Processing Standard (FIPS) code that is considered equivalent for this entity.
    /// </summary>
    public string EquivalentFipsCode { get; set; } = string.Empty;
}

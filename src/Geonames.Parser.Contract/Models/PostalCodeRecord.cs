using Geonames.Parser.Contract.Abstractions;

namespace Geonames.Parser.Contract.Models;

/// <summary>
/// Postal Code<br/>
/// https://download.geonames.org/export/zip/readme.txt
/// </summary>
public class PostalCodeRecord : IGeonameRecord
{
    /// <inheritdoc/>
    public static int NumberOfFields => 12;

    /// <summary>
    /// Iso country code, 2 characters
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;
    /// <summary>
    /// PostalCodeValue - varchar(20)
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;
    /// <summary>
    /// Place Name - varchar(180)
    /// </summary>
    public string PlaceName { get; set; } = string.Empty;
    /// <summary>
    /// admin name1 : 1. order subdivision (state) varchar(100)
    /// </summary>
    public string Admin1Name { get; set; } = string.Empty;
    /// <summary>
    /// admin code1 : 1. order subdivision(state) varchar(20)
    /// </summary>
    public string Admin1Code { get; set; } = string.Empty;
    /// <summary>
    /// admin name2 : 2. order subdivision(county/province) varchar(100)
    /// </summary>
    public string Admin2Name { get; set; } = string.Empty;
    /// <summary>
    /// admin code2 : 2. order subdivision(county/province) varchar(20)
    /// </summary>
    public string Admin2Code { get; set; } = string.Empty;
    /// <summary>
    /// admin name3 : 3. order subdivision(community) varchar(100)
    /// </summary>
    public string Admin3Name { get; set; } = string.Empty;
    /// <summary>
    /// admin code3 : 3. order subdivision(community) varchar(20)
    /// </summary>
    public string Admin3Code { get; set; } = string.Empty;
    /// <summary>
    /// latitude : estimated latitude(wgs84)
    /// </summary>
    public double Latitude { get; set; }
    /// <summary>
    /// longitude : estimated longitude(wgs84)
    /// </summary>
    public double Longitude { get; set; }
    /// <summary>
    /// accuracy : accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape
    /// </summary>
    public int Accuracy { get; set; }
}

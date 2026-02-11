using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Enums;

namespace Geonames.Parser.Contract.Models;

/// <summary>
/// Represents a single record from a GeoNames data file.
/// See: https://www.geonames.org/export/dump/readme.txt
/// </summary>
public class GeonameRecord : IGeonameRecord
{
    /// <inheritdoc/>
    public static int NumberOfFields => 19;

    /// <summary>
    /// geonameid : integer id of record in geonames database
    /// </summary>
    public int GeonameId { get; set; }

    /// <summary>
    /// name : name of geographical point (utf8) varchar(200)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// asciiname : name of geographical point in plain ascii characters, varchar(200)
    /// </summary>
    public string? AsciiName { get; set; }

    /// <summary>
    /// alternatenames : alternatenames,comma separated, ascii names automatically transliterated, convenience attribute from alternatename table, varchar(10000)
    /// </summary>
    public string? AlternateNames { get; set; }

    /// <summary>
    /// latitude : latitude in decimal degrees (wgs84)
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// longitude : longitude in decimal degrees (wgs84)
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// feature class : see http://www.geonames.org/export/codes.html, char(1)
    /// </summary>
    public GeonamesFeatureClass? FeatureClass { get; set; }

    /// <summary>
    /// feature code : see http://www.geonames.org/export/codes.html, varchar(10)
    /// </summary>
    public GeonamesFeatureCode? FeatureCode { get; set; }

    /// <summary>
    /// country code : ISO-3166 2-letter country code, 2 characters<br/>
    /// ISO of CountryInfoRecord
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// cc2 : alternate country codes, comma separated, ISO-3166 2-letter country code, 200 characters
    /// </summary>
    public string? Cc2 { get; set; }

    /// <summary>
    /// admin1 code : fipscode (subject to change to iso code), see exceptions below, varchar(20)
    /// </summary>
    public string? Admin1Code { get; set; }

    /// <summary>
    /// admin2 code : code for the second administrative division, a county in the US, see file admin2Codes.txt, varchar(80)
    /// </summary>
    public string? Admin2Code { get; set; }
    /// <summary>
    /// admin3 code : code for third level administrative division, varchar(20)
    /// </summary>
    public string? Admin3Code { get; set; }
    /// <summary>
    /// admin4 code : code for fourth level administrative division, varchar(20)
    /// </summary>
    public string? Admin4Code { get; set; }
    /// <summary>
    /// population : bigint (8 byte int)
    /// </summary>
    public long? Population { get; set; }
    /// <summary>
    /// elevation : in meters, integer
    /// </summary>
    public int? Elevation { get; set; }
    /// <summary>
    /// dem : digital elevation model, srtm3 or gtopo30, average elevation of 3''x3'' (ca 90mx90m) or 30''x30'' (ca 900mx900m) area in meters, integer. srtm processed by cgiar/ciat.
    /// </summary>
    public int? Dem { get; set; }
    /// <summary>
    /// timezone : the iana timezone id (see file timeZones.txt) varchar(40)
    /// </summary>
    public string? Timezone { get; set; }
    /// <summary>
    /// modification date : date of last modification in yyyy-MM-dd format
    /// </summary>
    public DateOnly? ModificationDate { get; set; }
}
namespace Geonames.Parser.Contract.Utility;

#pragma warning disable CS1591
public static class GeonamesUri
{
    public static string Admin1CodesUrl => "https://download.geonames.org/export/dump/admin1CodesASCII.txt";
    public static string Admin2CodesUrl => "https://download.geonames.org/export/dump/admin2Codes.txt";
    public static string CountryInfoUrl => "https://download.geonames.org/export/dump/countryInfo.txt";
    public static string GeonamesUrl(string iso) => $"https://download.geonames.org/export/dump/{iso}.zip";
    public static string AlternateNamesV2Url(string iso) => $"https://download.geonames.org/export/dump/alternatenames/{iso.ToUpperInvariant()}.zip";
    public static string AlternateNamesV2AllUrl() => $"https://download.geonames.org/export/dump/alternateNamesV2.zip";
    public static string TimeZoneUrl => "https://download.geonames.org/export/dump/timeZones.txt";
}
#pragma warning restore CS1591

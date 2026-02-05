namespace Geonames.Parser.Contract.Utility;

public static class GeonamesUri
{
    public static string Admin1CodesUrl => "https://download.geonames.org/export/dump/admin1CodesASCII.txt";
    public static string Admin2CodesUrl => "https://download.geonames.org/export/dump/admin2Codes.txt";
    public static string CountryInfoUrl => "https://download.geonames.org/export/dump/countryInfo.txt";
    public static string GeonamesUrl(string iso) => $"https://download.geonames.org/export/dump/{iso}.zip";
    public static string AlternateNamesV2Url(string iso) => $"https://download.geonames.org/export/dump/alternatenames/{iso.ToUpperInvariant()}.zip";
    public static string AlternateNamesV2AllUrl() => $"https://download.geonames.org/export/dump/alternateNamesV2.zip";
}

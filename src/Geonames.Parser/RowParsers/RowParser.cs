using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using System.Globalization;

namespace Geonames.Parser.RowParsers;

internal static class RowParser
{
    public static CountryInfoRecord? CountryInfo(int rowNumber,
        ReadOnlySpan<char> span,
        ref ParserResult result)
    {
        if (span.IsWhiteSpace() || span.StartsWith("#"))
        {
            return null; // Skip empty or commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[19];

        int count = span.Split(ranges, '\t');

        if (count != 19)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {rowNumber} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new CountryInfoRecord
            {
                ISO = span[ranges[0]].ToString(),
                ISO3 = span[ranges[1]].ToString(),
                ISO_Numeric = span[ranges[2]].ToString(),
                Fips = span[ranges[3]].ToString(),
                Country = span[ranges[4]].ToString(),
                Capital = span[ranges[5]].ToString(),
                Area = decimal.Parse(span[ranges[6]], CultureInfo.InvariantCulture),
                Population = long.Parse(span[ranges[7]], CultureInfo.InvariantCulture),
                Continent = span[ranges[8]].ToString(),
                Tld = span[ranges[9]].ToString(),
                CurrencyCode = span[ranges[10]].ToString(),
                CurrencyName = span[ranges[11]].ToString(),
                Phone = span[ranges[12]].ToString(),
                Postal_Code_Format = span[ranges[13]].ToString(),
                Postal_Code_Regex = span[ranges[14]].ToString(),
                Languages = span[ranges[15]].ToString(),
                GeonameId = int.Parse(span[ranges[16]], CultureInfo.InvariantCulture),
                Neighbours = span[ranges[17]].ToString(),
                EquivalentFipsCode = span[ranges[18]].ToString()
            };
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public static GeonameRecord? Geonames(
        ReadOnlySpan<char> span,
        ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty

        result.RecordsTotal++;
        if (span.StartsWith("#"))
        {
            return null; // Skip empty or commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[19];

        int count = span.Split(ranges, '\t');

        if (count != 19)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            var record = new GeonameRecord();

            var n = span[ranges[0]].ToString();
            record.GeonameId = int.Parse(n, CultureInfo.InvariantCulture);
            record.Name = span[ranges[1]].ToString();
            record.AsciiName = span[ranges[2]].ToString();
            record.AlternateNames = span[ranges[3]].ToString();
            record.Latitude = double.Parse(span[ranges[4]], CultureInfo.InvariantCulture);
            record.Longitude = double.Parse(span[ranges[5]], CultureInfo.InvariantCulture);
            record.FeatureClass = FieldParser.ParseEnum<GeonamesFeatureClass>(span[ranges[6]]);
            record.FeatureCode = FieldParser.ParseEnum<GeonamesFeatureCode>(span[ranges[7]]);
            record.CountryCode = span[ranges[8]].ToString();
            record.Cc2 = span[ranges[9]].ToString();
            record.Admin1Code = span[ranges[10]].ToString();
            record.Admin2Code = span[ranges[11]].ToString();
            record.Admin3Code = span[ranges[12]].ToString();
            record.Admin4Code = span[ranges[13]].ToString();
            record.Population = span[ranges[14]].IsEmpty ? null
                : long.Parse(span[ranges[14]], CultureInfo.InvariantCulture);
            record.Elevation = span[ranges[15]].IsEmpty ? null
                : int.Parse(span[ranges[15]], CultureInfo.InvariantCulture);
            record.Dem = span[ranges[16]].IsEmpty ? null
                : int.Parse(span[ranges[16]], CultureInfo.InvariantCulture);
            record.Timezone = span[ranges[17]].ToString();
            record.ModificationDate = FieldParser.ParseDateOnly(span[ranges[18]]);

            return record;
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }
}

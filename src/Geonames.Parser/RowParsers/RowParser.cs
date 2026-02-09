using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using System.Globalization;

namespace Geonames.Parser.RowParsers;

internal static class RowParser
{
    private static ReadOnlySpan<char> Tab => "\t".AsSpan();
    private static ReadOnlySpan<char> Hash => "#".AsSpan();
    private static string True => "1";
    private static ReadOnlySpan<char> Country => "Country".AsSpan();

    internal static CountryInfoRecord? CountryInfo(
        ReadOnlySpan<char> span,
        ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty
        result.RecordsTotal++;
        if (span.StartsWith(Hash))
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[19];
        int count = span.Split(ranges, Tab);
        if (count != 19)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
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
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }

    internal static GeonameRecord? Geonames(
        ReadOnlySpan<char> span,
        ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty

        result.RecordsTotal++;
        if (span.StartsWith(Hash))
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[19];

        int count = span.Split(ranges, Tab);

        if (count != 19)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new GeonameRecord
            {
                GeonameId = int.Parse(span[ranges[0]], CultureInfo.InvariantCulture),
                Name = span[ranges[1]].ToString(),
                AsciiName = span[ranges[2]].ToString(),
                AlternateNames = span[ranges[3]].ToString(),
                Latitude = double.Parse(span[ranges[4]], CultureInfo.InvariantCulture),
                Longitude = double.Parse(span[ranges[5]], CultureInfo.InvariantCulture),
                FeatureClass = FieldParser.ParseEnum<GeonamesFeatureClass>(span[ranges[6]]),
                FeatureCode = FieldParser.ParseEnum<GeonamesFeatureCode>(span[ranges[7]]),
                CountryCode = span[ranges[8]].ToString(),
                Cc2 = span[ranges[9]].ToString(),
                Admin1Code = span[ranges[10]].ToString(),
                Admin2Code = span[ranges[11]].ToString(),
                Admin3Code = span[ranges[12]].ToString(),
                Admin4Code = span[ranges[13]].ToString(),
                Population = span[ranges[14]].IsEmpty ? null
                    : long.Parse(span[ranges[14]], CultureInfo.InvariantCulture),
                Elevation = span[ranges[15]].IsEmpty ? null
                    : int.Parse(span[ranges[15]], CultureInfo.InvariantCulture),
                Dem = span[ranges[16]].IsEmpty ? null
                    : int.Parse(span[ranges[16]], CultureInfo.InvariantCulture),
                Timezone = span[ranges[17]].ToString(),
                ModificationDate = FieldParser.ParseDateOnly(span[ranges[18]])
            };
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }

    internal static Admin1CodeRecord? Admin1Code(ReadOnlySpan<char> span, ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty

        result.RecordsTotal++;
        if (span.StartsWith(Hash))
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[4];
        int count = span.Split(ranges, Tab);
        if (count != 4)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new Admin1CodeRecord
            {
                Code = span[ranges[0]].ToString(),
                Name = span[ranges[1]].ToString(),
                NameAscii = span[ranges[2]].ToString(),
                GeonameId = int.Parse(span[ranges[3]], CultureInfo.InvariantCulture)
            };
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }

    internal static Admin2CodeRecord? Admin2Code(ReadOnlySpan<char> span, ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty

        result.RecordsTotal++;
        if (span.StartsWith(Hash))
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[4];

        int count = span.Split(ranges, Tab);

        if (count != 4)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new Admin2CodeRecord
            {
                Code = span[ranges[0]].ToString(),
                Name = span[ranges[1]].ToString(),
                NameAscii = span[ranges[2]].ToString(),
                GeonameId = int.Parse(span[ranges[3]], CultureInfo.InvariantCulture)
            };
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }

    internal static AlternateNamesV2Record? AlternateNameV2(ReadOnlySpan<char> span, ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty
        result.RecordsTotal++;
        if (span.StartsWith(Hash))
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[10];
        int count = span.Split(ranges, Tab);
        if (count != 10)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new AlternateNamesV2Record
            {
                AlternateNameId = int.Parse(span[ranges[0]], CultureInfo.InvariantCulture),
                GeonameId = int.Parse(span[ranges[1]], CultureInfo.InvariantCulture),
                IsoLanguage = span[ranges[2]].ToString(),
                AlternateName = span[ranges[3]].ToString(),
                IsPreferredName = span[ranges[4]].ToString() == True,
                IsShortName = span[ranges[5]].ToString() == True,
                IsColloquial = span[ranges[6]].ToString() == True,
                IsHistoric = span[ranges[7]].ToString() == True,
                From = FieldParser.ParseDateOnly(span[ranges[8]]),
                To = FieldParser.ParseDateOnly(span[ranges[9]])
            };
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }

    internal static TimeZoneRecord? TimeZone(ReadOnlySpan<char> span, ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty
        result.RecordsTotal++;
        if (span.StartsWith(Hash) || span.StartsWith(Country)) // <= Header only fort Timezone file
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[5];
        int count = span.Split(ranges, Tab);
        if (count != 5)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new TimeZoneRecord
            {
                CountryCode = span[ranges[0]].ToString(),
                TimeZoneId = span[ranges[1]].ToString(),
                GMTOffset = double.Parse(span[ranges[2]], CultureInfo.InvariantCulture),
                DSTOffset = double.Parse(span[ranges[3]], CultureInfo.InvariantCulture),
                RawOffset = double.Parse(span[ranges[4]], CultureInfo.InvariantCulture)
            };
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }
}

using Geonames.Parser.Contract.Enums;
using Geonames.Parser.Contract.Models;
using Geonames.Parser.Contract.Utility;
using System.Globalization;

namespace Geonames.Parser.RowParsers;

internal static class RowParser
{
    private static readonly StringPool Pool = new StringPool();

    private static char Tab => '\t';
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

        Span<Range> ranges = stackalloc Range[CountryInfoRecord.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != CountryInfoRecord.NumberOfFields)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new CountryInfoRecord
            {
                ISO = Pool.GetOrAdd(span[ranges[0]]),
                ISO3 = Pool.GetOrAdd(span[ranges[1]]),
                ISO_Numeric = Pool.GetOrAdd(span[ranges[2]]),
                Fips = Pool.GetOrAdd(span[ranges[3]]),
                Country = Pool.GetOrAdd(span[ranges[4]]),
                Capital = span[ranges[5]].ToString(),
                Area = decimal.Parse(span[ranges[6]], CultureInfo.InvariantCulture),
                Population = long.Parse(span[ranges[7]], CultureInfo.InvariantCulture),
                Continent = Pool.GetOrAdd(span[ranges[8]]),
                Tld = Pool.GetOrAdd(span[ranges[9]]),
                CurrencyCode = Pool.GetOrAdd(span[ranges[10]]),
                CurrencyName = Pool.GetOrAdd(span[ranges[11]]),
                Phone = span[ranges[12]].ToString(),
                Postal_Code_Format = span[ranges[13]].ToString(),
                Postal_Code_Regex = span[ranges[14]].ToString(),
                Languages = span[ranges[15]].ToString(),
                GeonameId = int.Parse(span[ranges[16]], CultureInfo.InvariantCulture),
                Neighbours = span[ranges[17]].ToString(),
                EquivalentFipsCode = Pool.GetOrAdd(span[ranges[18]])
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

        Span<Range> ranges = stackalloc Range[GeonameRecord.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != GeonameRecord.NumberOfFields)
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
                CountryCode = Pool.GetOrAdd(span[ranges[8]]),
                Cc2 = Pool.GetOrAdd(span[ranges[9]]),
                Admin1Code = Pool.GetOrAdd(span[ranges[10]]),
                Admin2Code = Pool.GetOrAdd(span[ranges[11]]),
                Admin3Code = Pool.GetOrAdd(span[ranges[12]]),
                Admin4Code = Pool.GetOrAdd(span[ranges[13]]),
                Population = span[ranges[14]].IsEmpty ? null
                    : long.Parse(span[ranges[14]], CultureInfo.InvariantCulture),
                Elevation = span[ranges[15]].IsEmpty ? null
                    : int.Parse(span[ranges[15]], CultureInfo.InvariantCulture),
                Dem = span[ranges[16]].IsEmpty ? null
                    : int.Parse(span[ranges[16]], CultureInfo.InvariantCulture),
                Timezone = Pool.GetOrAdd(span[ranges[17]]),
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

        Span<Range> ranges = stackalloc Range[Admin1CodeRecord.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != Admin1CodeRecord.NumberOfFields)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new Admin1CodeRecord
            {
                Code = Pool.GetOrAdd(span[ranges[0]]),
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

        Span<Range> ranges = stackalloc Range[Admin2CodeRecord.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != Admin2CodeRecord.NumberOfFields)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new Admin2CodeRecord
            {
                Code = Pool.GetOrAdd(span[ranges[0]]),
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

        Span<Range> ranges = stackalloc Range[AlternateNamesV2Record.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != AlternateNamesV2Record.NumberOfFields)
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
                IsoLanguage = Pool.GetOrAdd(span[ranges[2]]),
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

        Span<Range> ranges = stackalloc Range[TimeZoneRecord.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != TimeZoneRecord.NumberOfFields)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new TimeZoneRecord
            {
                CountryCode = Pool.GetOrAdd(span[ranges[0]]),
                TimeZoneId = Pool.GetOrAdd(span[ranges[1]]),
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

    internal static PostalCodeRecord? PostalCode(ReadOnlySpan<char> span, ref ParserResult result)
    {
        if (span.IsWhiteSpace()) return null; // Skip empty
        result.RecordsTotal++;
        if (span.StartsWith(Hash) || span.StartsWith(Country)) // <= Header only fort Timezone file
        {
            return null; // Skip commented lines
        }
        result.RecordsFound++;

        Span<Range> ranges = stackalloc Range[PostalCodeRecord.NumberOfFields];
        int count = span.Split(ranges, Tab);
        if (count != PostalCodeRecord.NumberOfFields)
        {
            result.ErrorMessages.Add($"Skipping malformed row: {result.RecordsTotal} line: {span}");
            return null; // Not enough fields
        }

        try
        {
            return new PostalCodeRecord
            {
                CountryCode = Pool.GetOrAdd(span[ranges[0]]),
                PostalCode = span[ranges[1]].ToString(),
                PlaceName = span[ranges[2]].ToString(),
                Admin1Name = span[ranges[3]].ToString(),
                Admin1Code = Pool.GetOrAdd(span[ranges[4]]),
                Admin2Name = span[ranges[5]].ToString(),
                Admin2Code = Pool.GetOrAdd(span[ranges[6]]),
                Admin3Name = span[ranges[7]].ToString(),
                Admin3Code = Pool.GetOrAdd(span[ranges[8]]),
                Latitude = double.Parse(span[ranges[9]], CultureInfo.InvariantCulture),
                Longitude = double.Parse(span[ranges[10]], CultureInfo.InvariantCulture),
                Accuracy = int.Parse(span[ranges[11]], CultureInfo.InvariantCulture)
            };
        }
        catch (Exception e)
        {
            result.ErrorMessages.Add($"Skipping error parser row: {result.RecordsTotal} line: {e.Message}");
            return null;
        }
    }
}

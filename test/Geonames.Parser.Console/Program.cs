using Geonames.Parser;
using Geonames.Parser.Console;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;

Console.WriteLine("Geonames Test Console");

IDataProcessor processor = new ConsoleDataProcessor();
var parser = new GeonamesParser(processor);

ParserResult? result = null;
while (true)
{
    Console.WriteLine("== Parser console test ==");
    Console.WriteLine("1. Parsing Country Info data");
    Console.WriteLine("2. Parsing GeonamesData");
    Console.WriteLine("3. Parsing AlternateNamesV2");
    Console.WriteLine("4. Parsing Admin1Code");
    Console.WriteLine("5. Parsing Admin2Code");
    Console.WriteLine("6. Parsing TimeZone");
    var selectedOption = Console.ReadLine();

    try
    {
        if (selectedOption == "1")
        {
            result = await parser.ParseCountryInfoAsync();
        }
        else if (selectedOption == "2")
        {
            result = await TestingGeonamesParser(parser, result);
        }
        else if (selectedOption == "3")
        {
            (bool flowControl, result) = await TestingAlternateNameV2Parser(parser, result);
            if (!flowControl)
            {
                continue;
            }
        }
        else if (selectedOption == "4")
        {
            var admin1Filter = new Func<Admin1CodeRecord, bool>(record =>
            {
                return record.CountryCode == "US";
            });

            result = await parser.ParseAdmin1CodesAsync(admin1Filter);
        }
        else if (selectedOption == "5")
        {
            var admin1Filter = new Func<Admin2CodeRecord, bool>(record =>
            {
                return record.CountryCode == "US" && record.Admin1Code == "AL";
            });

            result = await parser.ParseAdmin2CodesAsync(admin1Filter);
        }
        else if (selectedOption == "6")
        {
            var admin1Filter = new Func<TimeZoneRecord, bool>(record =>
            {
                return record.CountryCode.StartsWith('I');
            });

            result = await parser.ParseTimeZoneDataAsync(admin1Filter);
        }
        else
        {
            Console.WriteLine("Invalid option selected.");
            break;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total row: {result.RecordsTotal}, Records Found: {result.RecordsFound}, Processed: {result.RecordsProcessed}, Added: {result.RecordsAdded}");
        if (result.ErrorMessages.Count != 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Errors encountered during parsing:");
            foreach (var error in result.ErrorMessages)
            {
                Console.WriteLine(error);
            }
        }
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"An error occurred: {ex.Message}");
        Console.ResetColor();
    }
}

static async Task<ParserResult> TestingGeonamesParser(
    IGeonamesParser parser,
    ParserResult? result)
{
    Console.WriteLine("Insert ISO code");
    var isoCode = Console.ReadLine() ?? string.Empty;

    Func<GeonameRecord, bool>? filter = null;

    //var filter = new Func<GeonameRecord, bool>(record =>
    //{
    //    // Example filter: Only include records with a population greater than 1000
    //    return record.FeatureClass == GeoNamesFeatureClass.P;
    //});

    //var filter = new Func<GeonameRecord, bool>(record =>
    //{
    //    return record.FeatureCode.HasValue
    //        && LocalizationFeatureCodes.PopulatedPlaces.Primary.Contains(record.FeatureCode.Value);
    //});

    result = await parser.ParseGeoNamesDataAsync(isoCode, filter);

    return result;
}

static async Task<(bool flowControl, ParserResult? value)> TestingAlternateNameV2Parser(
    IGeonamesParser parser,
    ParserResult? result)
{
    Console.WriteLine("Insert ISO code");
    var isoCode = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(isoCode) && isoCode.Length == 2)
    {
        static bool filters(AlternateNamesV2Record record)
        {
            return record.From.HasValue || record.To.HasValue;
        }

        result = await parser.ParseAlternateNamesV2DataAsync(isoCode, filters);
    }
    else
    {
        Console.WriteLine("Invalid ISO code.");
        return (flowControl: false, value: null);
    }

    return (flowControl: true, value: null);
}
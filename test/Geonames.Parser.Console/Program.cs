using Geonames.Parser;
using Geonames.Parser.Console;
using Geonames.Parser.Contract.Abstractions;
using Geonames.Parser.Contract.Models;

Console.WriteLine("Geonames Test Console");

IDataProcessor processor = new ConsoleDataProcessor();
var parser = new GeonamesParser();

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
    Console.WriteLine("7. Parsing Postal Code");
    var selectedOption = Console.ReadLine();

    try
    {
        if (selectedOption == "1")
        {
            result = await parser.ParseCountryInfoAsync(processor.ProcessCountryInfoRecordAsync,
                processor.FinalizeCountryInfoRecordAsync);
        }
        else if (selectedOption == "2")
        {
            result = await TestingGeonamesParser(parser);
        }
        else if (selectedOption == "3")
        {
            result = await TestingAlternateNameV2Parser(parser);
        }
        else if (selectedOption == "4")
        {
            var admin1Filter = new Func<Admin1CodeRecord, bool>(record =>
            {
                return record.CountryCode == "US";
            });

            result = await parser.ParseAdmin1CodesAsync(processor.ProcessAdmin1CodeRecordAsync,
                processor.FinalizeAdmin1CodeRecordAsync,
                filter: admin1Filter);
        }
        else if (selectedOption == "5")
        {
            var admin1Filter = new Func<Admin2CodeRecord, bool>(record =>
            {
                return record.CountryCode == "US" && record.Admin1Code == "AL";
            });

            result = await parser.ParseAdmin2CodesAsync(processor.ProcessAdmin2CodeRecordAsync,
                processor.FinalizeAdmin2CodeRecordAsync,
                filter: admin1Filter);
        }
        else if (selectedOption == "6")
        {
            var timeZoneFilter = new Func<TimeZoneRecord, bool>(record =>
            {
                return record.CountryCode.StartsWith('I');
            });

            result = await parser.ParseTimeZoneDataAsync(processor.ProcessTimeZoneRecordAsync,
                processor.FinalizeTimeZoneRecordAsync,
                filter: timeZoneFilter);
        }
        else if (selectedOption == "7")
        {
            result = await TestingPostalCode(parser);
        }
        else
        {
            Console.WriteLine("Invalid option selected.");
            break;
        }

        if (result == null)
        {
            Console.WriteLine("No result returned from the parser.");
            continue;
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

async Task<ParserResult> TestingGeonamesParser(
    IGeonamesParser parser)
{
    Console.Write("Insert ISO code or 'ALL' : ");
    var isoCode = Console.ReadLine() ?? string.Empty;

    Func<GeonameRecord, bool>? filter = null;

    ////var filter = new Func<GeonameRecord, bool>(record =>
    ////{
    ////    return record.FeatureCode == Geonames.Parser.Contract.Enums.GeonamesFeatureCode.ADM3;
    ////});

    ////var filter = new Func<GeonameRecord, bool>(record =>
    ////{
    ////    return record.FeatureCode.HasValue
    ////        && LocalizationFeatureCodes.PopulatedPlaces.Primary.Contains(record.FeatureCode.Value);
    ////});

    result = await parser.ParseGeoNamesDataAsync(isoCode,
        processor.ProcessGeoNameRecordAsync,
        processor.FinalizeGeoNameRecordAsync,
        filter);

    return result;
}

async Task<ParserResult?> TestingAlternateNameV2Parser(IGeonamesParser parser)
{
    Console.Write("Insert ISO code or 'ALL' : ");
    var isoCode = Console.ReadLine() ?? string.Empty;

    var filters = new Func<AlternateNamesV2Record, bool>(record =>
    {
        return !string.IsNullOrEmpty(record.IsoLanguage) && record.IsoLanguage.Length == 2;
    });

    ////var filters = new Func<AlternateNamesV2Record, bool>(record =>
    ////{
    ////    return record.From.HasValue || record.To.HasValue;
    ////});

    result = await parser.ParseAlternateNamesV2DataAsync(isoCode,
        processor.ProcessAlternateNamesV2RecordAsync,
        processor.FinalizeAlternateNamesV2RecordAsync,
        filter: filters);

    return result;
}

async Task<ParserResult?> TestingPostalCode(GeonamesParser parser)
{
    Console.Write("Insert ISO code or 'ALL' ");
    var isoCode = Console.ReadLine() ?? string.Empty;

    var filter = new Func<PostalCodeRecord, bool>(record =>
    {
        return true; // Example filter: Include all records
    });

    result = await parser.ParsePostalCodeDataAsync(isoCode,
        processor.ProcessPostalCodeRecordAsync,
        processor.FinalizePostalCodeRecordAsync,
        filter: filter);
    return result;
}
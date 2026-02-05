namespace Geonames.Parser.Contract.Models;

public class AlternateNamesV2Record
{
    /// <summary>
    /// alternateNameId : the id of this alternate name, int
    /// </summary>
    public int AlternateNameId { get; set; }
    /// <summary>
    /// geonameid : geonameId referring to id in table 'geoname', int
    /// </summary>
    public int GeonameId { get; set; }
    /// <summary>
    /// isolanguage : iso 639 language code 2- or 3-characters, <br/>
    /// optionally followed by a hyphen and a countrycode for country specific variants(ex:zh-CN) or by a variant name(ex: zh-Hant); <br/>
    /// 4-characters 'post' for postal codes and 'iata','icao' and faac for airport codes, <br/>
    /// fr_1793 for French Revolution names,<br/>
    /// abbr for abbreviation, link to a website(mostly to wikipedia), wkdt for the wikidataid, varchar(7)
    /// </summary>
    public string? IsoLanguage { get; set; }
    /// <summary>
    /// alternate name : alternate name or name variant, varchar(400)
    /// </summary>
    public string AlternateName { get; set; } = null!;
    /// <summary>
    /// isPreferredName : '1', if this alternate name is an official/preferred name
    /// </summary>
    public bool IsPreferredName { get; set; }
    /// <summary>
    /// isShortName : '1', if this is a short name like 'California' for 'State of California'
    /// </summary>
    public bool IsShortName { get; set; }
    /// <summary>
    /// isColloquial : '1', if this alternate name is a colloquial or slang term.Example: 'Big Apple' for 'New York'.
    /// </summary>
    public bool IsColloquial { get; set; }
    /// <summary>
    /// isHistoric : '1', if this alternate name is historic and was used in the past.Example 'Bombay' for 'Mumbai'.
    /// </summary>
    public bool IsHistoric { get; set; }
    /// <summary>
    /// from : from period when the name was used<br/>
    /// yyyy or yyyy-MM-dd
    /// </summary>
    public DateOnly? From { get; set; }
    /// <summary>
    /// to : to period when the name was used
    /// yyyy or yyyy-MM-dd
    /// </summary>
    public DateOnly? To { get; set; }
}

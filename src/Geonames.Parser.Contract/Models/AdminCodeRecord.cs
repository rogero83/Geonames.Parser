using Geonames.Parser.Contract.Abstractions;

namespace Geonames.Parser.Contract.Models;

/// <summary>
/// Gets the second-level administrative division code associated with this record.
/// </summary>
public class Admin2CodeRecord : Admin1CodeRecord
{
    private string? _admin2Code;
    /// <summary>
    /// Admin2Code
    /// </summary>
    public string Admin2Code
    {
        get
        {
            if (_admin2Code is null)
            {
                var parts = Code.Split('.');
                _admin2Code = parts[2];
            }
            return _admin2Code;
        }
    }
}

/// <summary>
/// Gets the first-level administrative division code associated with this record.
/// </summary>
public class Admin1CodeRecord : AdminXCodeRecord
{
    private string? _countryCode;
    /// <summary>
    /// Country Code ISO alpha-2
    /// </summary>
    public string CountryCode
    {
        get
        {
            if (_countryCode is null)
            {
                var parts = Code.Split('.');
                _countryCode = parts[0];
            }

            return _countryCode;
        }
    }

    private string? _admin1Code;
    /// <summary>
    /// Admin1Code
    /// </summary>
    public string Admin1Code
    {
        get
        {
            if (_admin1Code is null)
            {
                var parts = Code.Split('.');
                _admin1Code = parts[1];
            }
            return _admin1Code;
        }
    }
}

/// <summary>
/// The abstract class for generic administrative division code.
/// </summary>
public abstract class AdminXCodeRecord : IGeonameRecord
{
    /// <inheritdoc/>
    public static int NumberOfFields => 4;

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; } = null!;
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Ascii Name
    /// </summary>
    public string? NameAscii { get; set; }
    /// <summary>
    /// GeonameId
    /// </summary>
    public int GeonameId { get; set; }
}

namespace Geonames.Parser.Contract.Models;

public class Admin2CodeRecord : Admin1CodeRecord
{
    private string? _admin2Code;
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

public class Admin1CodeRecord : AdminXCodeRecord
{
    private string? _countryCode;
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

public class AdminXCodeRecord
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? NameAscii { get; set; }
    public int GeonameId { get; set; }
}

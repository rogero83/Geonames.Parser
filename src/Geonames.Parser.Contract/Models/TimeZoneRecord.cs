using Geonames.Parser.Contract.Abstractions;

namespace Geonames.Parser.Contract.Models;

/// <summary>
/// Time zone record representing the time zone information for a specific country.
/// </summary>
public class TimeZoneRecord : IGeonameRecord
{
    /// <summary>
    /// Country code ISO-3166 2-letter
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;
    /// <summary>
    /// TimeZone ID (e.g. "Europe/Rome")
    /// </summary>
    public string TimeZoneId { get; set; } = string.Empty;
    /// <summary>
    /// GMT offset 1. Jan 2026
    /// </summary>
    public double GMTOffset { get; set; }
    /// <summary>
    /// DST offset 1. Jul 2026
    /// </summary>
    public double DSTOffset { get; set; }
    /// <summary>
    /// rawOffset (independant of DST)
    /// </summary>
    public double RawOffset { get; set; }
}

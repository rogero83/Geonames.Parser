using System.Globalization;

namespace Geonames.Parser.Contract.Utility;

/// <summary>
/// Field parser utility
/// </summary>
public static class FieldParser
{
    /// <summary>
    /// Parse string input to DateOnly nullable
    /// </summary>
    public static DateOnly? ParseDateOnly(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        return ParseDateOnly(input.AsSpan());
    }

    /// <summary>
    /// Parse span input to DateOnly nullable
    /// </summary>
    public static DateOnly? ParseDateOnly(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
            return null;

        // Case 1: Only year (es. "2024")
        if (input.Length == 4 && int.TryParse(input, out int year))
        {
            return new DateOnly(year, 1, 1);
        }

        // Case 2: Full data yyyy-MM-dd
        // DateOnly.TryParseExact supports ReadOnlySpan<char>
        if (DateOnly.TryParseExact(input, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
        {
            return date;
        }

        return null;
    }

    /// <summary>
    /// Parse string input to enum nullable
    /// </summary>
    public static TEnum? ParseEnum<TEnum>(string? input) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(input))
            return default;

        return ParseEnum<TEnum>(input.AsSpan());
    }

    /// <summary>
    /// Parse span input to enum nullable
    /// </summary>
    public static TEnum? ParseEnum<TEnum>(ReadOnlySpan<char> input) where TEnum : struct, Enum
    {
        if (input.IsEmpty)
            return default;

        // Enum.TryParse supports ReadOnlySpan<char> in .NET Core 2.1+ / Standard 2.1+
        return Enum.TryParse<TEnum>(input, true, out var result)
            ? result
            : null;
    }
}

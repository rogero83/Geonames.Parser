namespace Geonames.Parser.RowParsers;

/// <summary>
/// A fast, lock-free, allocation-free string pool for deduplicating repeated strings during parsing.
/// Uses a very simple hashing algorithm to index into a static array.
/// </summary>
internal sealed class StringPool
{
    private readonly string?[] _pool;
    private readonly int _mask;

    /// <summary>
    /// Initializes a new instance of the StringPool with a specified power-of-two capacity.
    /// </summary>
    /// <param name="capacity">Must be a power of two (e.g., 8192, 16384).</param>
    public StringPool(int capacity = 8192)
    {
        if ((capacity & (capacity - 1)) != 0)
        {
            throw new ArgumentException("Capacity must be a power of two.", nameof(capacity));
        }

        _pool = new string?[capacity];
        _mask = capacity - 1;
    }

    /// <summary>
    /// Gets a pooled string equal to the provided span, or allocates a new one and adds it to the pool.
    /// </summary>
    public string GetOrAdd(ReadOnlySpan<char> span)
    {
        if (span.IsEmpty)
        {
            return string.Empty;
        }

        int hash = GetHashCode(span);
        int index = hash & _mask;

        string? existing = _pool[index];
        if (existing != null && span.SequenceEqual(existing.AsSpan()))
        {
            return existing;
        }

        string newString = new string(span);
        // We write the new string reference to the array.
        // In C# this assignment is atomic, so race conditions from multiple threads 
        // parsing concurrently just overwrite the slot safely without breaking memory.
        _pool[index] = newString;

        return newString;
    }

    // A fast custom hash function tuned for short strings (like ISOCodes, Timezones).
    // Using FNV-1a or djb2 variant.
    private static int GetHashCode(ReadOnlySpan<char> span)
    {
        int hash = 5381;
        for (int i = 0; i < span.Length; i++)
        {
            hash = ((hash << 5) + hash) ^ span[i];
        }
        return hash;
    }
}

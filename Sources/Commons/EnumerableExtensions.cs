namespace Commons;

public static class EnumerableExtensions
{
    public static async Task<List<TValue>> ToListAsync<TValue>(this IAsyncEnumerable<TValue> enumerable, CancellationToken cancellationToken)
    {
        var result = new List<TValue>();
        await foreach (var value in enumerable.WithCancellation(cancellationToken))
        {
            result.Add(value);
        }

        return result;
    }

    public static string JoinStrings<TValue>(this IEnumerable<TValue> enumerable, string delimiter)
    {
        return string.Join(delimiter, enumerable);
    }

    public static bool IsNotEmpty<TValue>(this IEnumerable<TValue> enumerable)
    {
        return enumerable != null! && enumerable.Any();
    }
}
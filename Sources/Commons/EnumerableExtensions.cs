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

    public static void ForEach<TValue>(this IEnumerable<TValue> enumerable, Action<TValue> action)
    {
        foreach (var value in enumerable)
        {
            action(value);
        }
    }

    public static void ForEach<TValue>(this IEnumerable<TValue> enumerable, Action<TValue, int> action)
    {
        foreach (var (value, index) in enumerable.Select((value, index) => (value, index)))
        {
            action(value, index);
        }
    }

    public static string JoinStrings<TValue>(this IEnumerable<TValue> enumerable, string delimiter)
    {
        return string.Join(delimiter, enumerable);
    }

    public static bool IsNotEmpty<TValue>(this IEnumerable<TValue> enumerable)
    {
        return enumerable != null! && enumerable.Any();
    }

    public static IEnumerable<TValue> NotNull<TValue>(this IEnumerable<TValue> enumerable)
    {
        return enumerable.Where(t => t is not null);
    }
    
    public static IEnumerable<TValue> NotNull<TValue>(this IEnumerable<TValue?> enumerable)
        where TValue : struct
    {
        return enumerable.Where(t => t is not null).Select(t => t!.Value);
    }
}
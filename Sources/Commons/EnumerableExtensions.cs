namespace Commons;

public static class EnumerableExtensions
{
    public static string JoinStrings<TValue>(this IEnumerable<TValue> enumerable, string delimiter)
    {
        return string.Join(delimiter, enumerable);
    }

    public static bool IsNotEmpty<TValue>(this IEnumerable<TValue> enumerable)
    {
        return enumerable != null! && enumerable.Any();
    }
}
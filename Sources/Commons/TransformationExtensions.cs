namespace Commons;

public static class TransformationExtensions
{
    public static TResult Map<TValue, TResult>(this TValue value, Func<TValue, TResult> map)
    {
        return map(value);
    }
}
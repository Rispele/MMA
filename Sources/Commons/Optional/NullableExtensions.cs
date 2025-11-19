namespace Commons.Optional;

public static class NullableExtensions
{
    public static Optional<TValue> AsOptional<TValue>(this TValue? value)
        where TValue : class
    {
        return new Optional<TValue>(value);
    }
    
    public static OptionalStruct<TValue> AsOptional<TValue>(this TValue? value)
        where TValue : struct
    {
        return new OptionalStruct<TValue>(value);
    }
}
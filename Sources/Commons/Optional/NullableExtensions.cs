namespace Commons.Optional;

public static class NullableExtensions
{
    public static Optional<TValue> AsOptional<TValue>(this TValue? value)
    {
        return new Optional<TValue>(value);
    }
}
namespace Commons;

public record Optional<TValue>(TValue? Value)
{
    public Optional<TResult> Map<TResult>(Func<TValue, TResult> map)
        where TResult: class
    {
        return Value is null
            ? ((TResult?)null).AsOptional()
            : map(Value).AsOptional();
    }

    public TValue OrElse(Func<TValue> provider)
    {
        return Value is null ? provider() : Value;
    }

    public TValue OrElseThrow(Exception exception)
    {
        return Value is null ? throw exception : Value;
    }

    public static implicit operator TValue? (Optional<TValue> optional)
    {
        return optional.Value;
    }
}
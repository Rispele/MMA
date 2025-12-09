namespace Commons.Optional;

public record Optional<TValue>(TValue? Value)
    where TValue : class
{
    public Optional<TResult> Map<TResult>(Func<TValue, TResult?> map)
        where TResult : class
    {
        return Value is null
            ? ((TResult?)null).AsOptional()
            : map(Value).AsOptional();
    }

    public TApplicable Apply<TApplicable>(TApplicable applicable, Func<TApplicable, TValue, TApplicable> apply)
    {
        return Value is not null ? apply(applicable, Value) : applicable;
    }

    public TValue OrElse(Func<TValue> provider)
    {
        return Value ?? provider();
    }

    public TValue OrElseThrow(Exception exception)
    {
        return Value ?? throw exception;
    }

    public static implicit operator TValue?(Optional<TValue> optional)
    {
        return optional.Value;
    }
}
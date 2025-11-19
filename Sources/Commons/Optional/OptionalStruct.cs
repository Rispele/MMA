namespace Commons.Optional;

public record OptionalStruct<TValue>(TValue? Value)
    where TValue : struct
{
    public Optional<TResult> Map<TResult>(Func<TValue, TResult> map)
        where TResult : class
    {
        return !Value.HasValue
            ? ((TResult?)null).AsOptional()
            : map(Value.Value).AsOptional();
    }

    public TApplicable Apply<TApplicable>(TApplicable applicable, Func<TApplicable, TValue, TApplicable> apply)
    {
        return Value is not null ? apply(applicable, Value.Value) : applicable;
    }

    public TValue OrElse(Func<TValue> provider)
    {
        return Value ?? provider();
    }

    public TValue OrElseThrow(Exception exception)
    {
        return Value ?? throw exception;
    }

    public static implicit operator TValue?(OptionalStruct<TValue> optional)
    {
        return optional.Value;
    }
}
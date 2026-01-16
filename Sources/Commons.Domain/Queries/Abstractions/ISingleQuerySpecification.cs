namespace Commons.Domain.Queries.Abstractions;

public interface ISingleQuerySpecification<out TSelf, out TEntity>
    where TSelf : class, ISingleQuerySpecification<TSelf, TEntity>
{
    public TSelf Self()
    {
        return this as TSelf ??
               throw new InvalidCastException(
                   $"Invalid single query specification configuration. TSelf must be [{GetType().Name}], but was [{typeof(TSelf).Name}]");
    }
}
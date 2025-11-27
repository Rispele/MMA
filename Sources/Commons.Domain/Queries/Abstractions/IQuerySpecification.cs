namespace Commons.Domain.Queries.Abstractions;

public interface IQuerySpecification<out TSelf, out TEntity>
    where TSelf : class, IQuerySpecification<TSelf, TEntity>
{
    public TSelf Self()
    {
        return this as TSelf ??
               throw new InvalidCastException(
                   $"Invalid query specification configuration. TSelf must be [{GetType().Name}], but was [{typeof(TSelf).Name}]");
    }
}
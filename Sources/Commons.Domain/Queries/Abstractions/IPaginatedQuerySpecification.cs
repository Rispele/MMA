namespace Commons.Domain.Queries.Abstractions;

public interface IPaginatedQuerySpecification<out TSelf, out TEntity>
    where TSelf : class, IPaginatedQuerySpecification<TSelf, TEntity>
{
    public TSelf Self()
    {
        return this as TSelf ??
               throw new InvalidCastException(
                   $"Invalid paginated query specification configuration. TSelf must be [{GetType().Name}], but was [{typeof(TSelf).Name}]");
    }
}
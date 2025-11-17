namespace Rooms.Core.Queries.Abstractions;

public interface IQuerySpecification<out TSelf, out TEntity>
    where TSelf : class, IQuerySpecification<TSelf, TEntity>
{
    public TSelf Self() => this as TSelf ??
                           throw new InvalidCastException(
                               $"Invalid query specification configuration. TSelf must be [{GetType().Name}], but was [{typeof(TSelf).Name}]");
}
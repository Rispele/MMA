namespace Rooms.Domain.Queries.Abstractions;

public interface IQueryObject<out TEntity, in TSource>
{
    public IAsyncEnumerable<TEntity> Apply(TSource source);
}
namespace Rooms.Persistence.Queries.Abstractions;

public interface IQueryImplementer<out TEntity, in TSource>
{
    public IAsyncEnumerable<TEntity> Apply(TSource source);
}
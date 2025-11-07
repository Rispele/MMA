namespace Rooms.Infrastructure.Queries.Abstractions;

public interface IQueryImplementer<out TEntity, in TSource>
{
    public IAsyncEnumerable<TEntity> Apply(TSource source);
}
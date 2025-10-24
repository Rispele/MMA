namespace Rooms.Persistence.Queries.Abstractions;

public interface ISingleQueryImplementer<TEntity, in TSource>
{
    public Task<TEntity> Apply(TSource source, CancellationToken cancellationToken);
}
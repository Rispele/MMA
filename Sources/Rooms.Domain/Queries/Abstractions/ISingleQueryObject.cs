namespace Rooms.Domain.Queries.Abstractions;

public interface ISingleQueryObject <TEntity, in TSource>
    where TSource : IModelsSource
{
    public Task<TEntity> Apply(TSource source, CancellationToken cancellationToken);
}
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries;

public static class QueryExtensions
{
    public static IAsyncEnumerable<TEntity> ApplyQuery<TEntity, TSource>(
        this TSource source,
        IQueryObject<TEntity, TSource> queryObject)
        where TSource : IModelsSource
    {
        return queryObject.Apply(source);
    }

    public static Task<TEntity> ApplyQuery<TEntity, TSource>(
        this TSource source,
        ISingleQueryObject<TEntity, TSource> queryObject,
        CancellationToken cancellationToken)
        where TSource : IModelsSource
    {
        return queryObject.Apply(source, cancellationToken);
    }
}
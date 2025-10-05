using Commons.Queries.Abstractions;

namespace Commons.Queries;

public static class QueryExtensions
{
    public static IAsyncEnumerable<TEntity> ApplyQuery<TEntity, TDbContext>(
        this TDbContext dbContext,
        IQueryObject<TEntity, TDbContext> queryObject)
    {
        return queryObject.Apply(dbContext);
    }

    public static Task<TEntity> ApplyQuery<TEntity, TDbContext>(
        this TDbContext dbContext,
        ISingleQueryObject<TEntity, TDbContext> queryObject,
        CancellationToken cancellationToken)
    {
        return queryObject.Apply(dbContext, cancellationToken);
    }
}
namespace Commons.Queries.Abstractions;

public interface ISingleQueryObject <TEntity, in TDbContext>
{
    public Task<TEntity> Apply(TDbContext dbContext, CancellationToken cancellationToken);
}
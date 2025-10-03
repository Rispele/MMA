namespace Commons.Queries.Abstractions;

public interface IQueryObject<out TEntity, in TDbContext>
{
    public IAsyncEnumerable<TEntity> Apply(TDbContext dbContext);
}
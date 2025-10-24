namespace Rooms.Domain.Queries.Abstractions;

public interface IUnitOfWork : IAsyncDisposable
{
    public IAsyncEnumerable<TEntity> ApplyQuery<TEntity>(
        IQuerySpecification<TEntity> querySpecification);

    public Task<TEntity> ApplyQuery<TEntity>(
        ISingleQuerySpecification<TEntity> querySpecification,
        CancellationToken cancellationToken);

    public void Add<TEntity>(TEntity entity)
        where TEntity : class;
    
    public void Update<TEntity>(TEntity entity)
        where TEntity : class;
    
    public Task Commit(CancellationToken cancellationToken = default);
}
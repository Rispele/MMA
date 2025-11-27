namespace Rooms.Core.Queries.Abstractions;

public interface IUnitOfWork : IAsyncDisposable
{
    public Task<IAsyncEnumerable<TEntity>> ApplyQuery<TQuery, TEntity>(
        IQuerySpecification<TQuery, TEntity> querySpecification,
        CancellationToken cancellationToken)
        where TQuery : class, IQuerySpecification<TQuery, TEntity>;

    public Task<TEntity> ApplyQuery<TQuery, TEntity>(
        ISingleQuerySpecification<TQuery, TEntity> querySpecification,
        CancellationToken cancellationToken)
        where TQuery : class, ISingleQuerySpecification<TQuery, TEntity>;

    public void Add<TEntity>(TEntity entity)
        where TEntity : class;

    public void Update<TEntity>(TEntity entity)
        where TEntity : class;

    public Task Commit(CancellationToken cancellationToken = default);
}
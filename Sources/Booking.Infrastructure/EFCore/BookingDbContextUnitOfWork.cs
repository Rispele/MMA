using Commons.Domain.Queries.Abstractions;
using Commons.Infrastructure.EFCore.QueryHandlers;
using MediatR;

namespace Booking.Infrastructure.EFCore;

internal class BookingDbContextUnitOfWork(BookingDbContext dbContext, IMediator mediator) : IUnitOfWork
{
    public Task<IAsyncEnumerable<TEntity>> ApplyQuery<TQuery, TEntity>(
        IQuerySpecification<TQuery, TEntity> querySpecification,
        CancellationToken cancellationToken)
        where TQuery : class, IQuerySpecification<TQuery, TEntity>
    {
        var query = new EntityQuery<BookingDbContext, TQuery, IAsyncEnumerable<TEntity>>(querySpecification.Self(), dbContext);

        return mediator.Send(query, cancellationToken);
    }

    public Task<TEntity> ApplyQuery<TQuery, TEntity>(
        ISingleQuerySpecification<TQuery, TEntity> querySpecification,
        CancellationToken cancellationToken)
        where TQuery : class, ISingleQuerySpecification<TQuery, TEntity>
    {
        var query = new EntityQuery<BookingDbContext, TQuery, TEntity>(querySpecification.Self(), dbContext);

        return mediator.Send(query, cancellationToken);
    }

    public void Add<TEntity>(TEntity entity)
        where TEntity : class
    {
        var set = dbContext.Set<TEntity>();

        set.Add(entity);
    }

    public void Update<TEntity>(TEntity entity)
        where TEntity : class
    {
        var set = dbContext.Set<TEntity>();

        set.Update(entity);
    }

    public Task Commit(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
    }
}
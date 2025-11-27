using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commons.Infrastructure.EFCore.QueryHandlers;

public interface
    IQueryHandler<TContext, TCommand, TEntity> : IRequestHandler<EntityQuery<TContext, TCommand, IAsyncEnumerable<TEntity>>,
    IAsyncEnumerable<TEntity>>
    where TContext : DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commons.Infrastructure.EFCore.QueryHandlers;

public interface IPaginatedQueryHandler<TContext, TCommand, TEntity>
    : IRequestHandler<EntityQuery<TContext, TCommand, (IAsyncEnumerable<TEntity>, int)>, (IAsyncEnumerable<TEntity>, int)>
    where TContext : DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commons.Infrastructure.EFCore.QueryHandlers;

public interface ISingleQueryHandler<TContext, TCommand, TEntity> : IRequestHandler<EntityQuery<TContext, TCommand, TEntity>, TEntity>
    where TContext : DbContext;
using MediatR;

namespace Rooms.Infrastructure.EFCore.QueryHandlers;

public interface IQueryHandler<TCommand, TEntity> : IRequestHandler<EntityQuery<TCommand, IAsyncEnumerable<TEntity>>, IAsyncEnumerable<TEntity>>;
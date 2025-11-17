using MediatR;

namespace Rooms.Infrastructure.QueryHandlers;

public interface IQueryHandler<TCommand, TEntity> : IRequestHandler<EntityQuery<TCommand, IAsyncEnumerable<TEntity>>, IAsyncEnumerable<TEntity>>;
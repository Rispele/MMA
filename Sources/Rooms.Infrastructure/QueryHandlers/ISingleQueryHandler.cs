using MediatR;

namespace Rooms.Infrastructure.QueryHandlers;

public interface ISingleQueryHandler<TCommand, TEntity> : IRequestHandler<EntityQuery<TCommand, TEntity>, TEntity>;
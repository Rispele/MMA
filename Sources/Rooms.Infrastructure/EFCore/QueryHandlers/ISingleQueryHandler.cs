using MediatR;

namespace Rooms.Infrastructure.EFCore.QueryHandlers;

public interface ISingleQueryHandler<TCommand, TEntity> : IRequestHandler<EntityQuery<TCommand, TEntity>, TEntity>;
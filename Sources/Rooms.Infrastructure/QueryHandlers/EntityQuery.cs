using MediatR;

namespace Rooms.Infrastructure.QueryHandlers;

public record EntityQuery<TQuery, TResponse>(TQuery Query, RoomsDbContext Context) : IRequest<TResponse>;
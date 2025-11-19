using MediatR;

namespace Rooms.Infrastructure.EFCore.QueryHandlers;

public record EntityQuery<TQuery, TResponse>(TQuery Query, RoomsDbContext Context) : IRequest<TResponse>;
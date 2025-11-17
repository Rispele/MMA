using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Room;

namespace Rooms.Infrastructure.QueryHandlers.Rooms;

public class FindRoomsByIdQueryHandler : IQueryHandler<FindRoomsByIdQuery, Room>
{
    public Task<IAsyncEnumerable<Room>> Handle(EntityQuery<FindRoomsByIdQuery, IAsyncEnumerable<Room>> request, CancellationToken cancellationToken)
    {
        var ids = request.Query.RoomIds;

        var response = request.Context.Rooms
            .Where(predicate: t => ids.Contains(t.Id))
            .AsAsyncEnumerable();
        
        return Task.FromResult(response);
    }
}
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

public class FindRoomsByIdQueryHandler : IQueryHandler<FindRoomsByIdQuery, Room>
{
    public Task<IAsyncEnumerable<Room>> Handle(EntityQuery<FindRoomsByIdQuery, IAsyncEnumerable<Room>> request, CancellationToken cancellationToken)
    {
        var ids = request.Query.RoomIds;

        var response = request.Context.Rooms
            .Include(room => room.Equipments)
            .Where(predicate: t => ids.Contains(t.Id))
            .AsAsyncEnumerable();

        return Task.FromResult(response);
    }
}
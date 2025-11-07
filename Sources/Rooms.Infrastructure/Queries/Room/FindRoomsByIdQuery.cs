using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.Room;

public readonly struct FindRoomsByIdQuery :
    IFindRoomsByIdQuery,
    IQueryImplementer<Domain.Models.Room.Room, RoomsDbContext>
{
    public required IEnumerable<int> RoomIds { get; init; }

    public IAsyncEnumerable<Domain.Models.Room.Room> Apply(RoomsDbContext source)
    {
        var ids = RoomIds;

        return source.Rooms
            .Where(predicate: t => ids.Contains(t.Id))
            .AsAsyncEnumerable();
    }
}
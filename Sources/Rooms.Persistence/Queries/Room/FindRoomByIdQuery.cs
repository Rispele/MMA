using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Implementations.Room;

namespace Rooms.Persistence.Queries.Room;

public readonly struct FindRoomByIdQuery : IFindRoomByIdQuery
{
    public required int RoomId { get; init; }

    public Task<Domain.Models.Room.Room?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        var id = RoomId;

        return source.Rooms.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
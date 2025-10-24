using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Implementations.Rooms;

namespace Rooms.Persistence.Queries.Room;

public readonly struct FindRoomByIdQuery: IFindRoomByIdQuery<RoomsDbContext>
{
    public required int RoomId { get; init; }

    public Task<Domain.Models.RoomModels.Room?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        var id = RoomId;

        return source.Rooms.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }
}
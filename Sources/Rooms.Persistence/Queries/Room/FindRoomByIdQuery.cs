using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.Room;

public readonly struct FindRoomByIdQuery :
    IFindRoomByIdQuery,
    ISingleQueryImplementer<Domain.Models.Room.Room?, RoomsDbContext>
{
    public required int RoomId { get; init; }

    public Task<Domain.Models.Room.Room?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        var id = RoomId;

        return source.Rooms
            .Include(x => x.Equipments)
            .ThenInclude(x => x.Schema)
            .ThenInclude(x => x.Type)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
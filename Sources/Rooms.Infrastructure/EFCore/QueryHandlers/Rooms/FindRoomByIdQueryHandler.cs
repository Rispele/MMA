using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

public class FindRoomByIdQueryHandler : ISingleQueryHandler<FindRoomByIdQuery, Room?>
{
    public Task<Room?> Handle(EntityQuery<FindRoomByIdQuery, Room?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.RoomId;

        return request.Context.Rooms
            .Include(room => room.Equipments)
            .ThenInclude(x => x.Schema)
            .ThenInclude(x => x.Type)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
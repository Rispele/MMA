using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

public class FindRoomByNameQueryHandler : ISingleQueryHandler<FindRoomByNameQuery, Room?>
{
    public Task<Room?> Handle(EntityQuery<FindRoomByNameQuery, Room?> request, CancellationToken cancellationToken)
    {
        var name = request.Query.Name;

        return request.Context.Rooms
            .Include(room => room.Equipments)
            .FirstOrDefaultAsync(predicate: t => t.Name == name, cancellationToken);
    }
}
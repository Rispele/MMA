using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

internal class FindRoomByNameQueryHandler : ISingleQueryHandler<RoomsDbContext, FindRoomByNameQuery, Room?>
{
    public Task<Room?> Handle(EntityQuery<RoomsDbContext, FindRoomByNameQuery, Room?> request, CancellationToken cancellationToken)
    {
        var name = request.Query.Name;

        return request.Context.Rooms
            .Include(room => room.Equipments)
            .FirstOrDefaultAsync(predicate: t => t.Name == name, cancellationToken);
    }
}
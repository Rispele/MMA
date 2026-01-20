using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

internal class FindRoomByIdQueryHandler : ISingleQueryHandler<RoomsDbContext, FindRoomByIdQuery, Room?>
{
    public Task<Room?> Handle(EntityQuery<RoomsDbContext, FindRoomByIdQuery, Room?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.RoomId;

        return request.Context.Rooms
            .Include(room => room.Equipments)
            .ThenInclude(equipment => equipment.Schema)
            .ThenInclude(schema => schema.Type)
            .Include(room => room.OperatorDepartment)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
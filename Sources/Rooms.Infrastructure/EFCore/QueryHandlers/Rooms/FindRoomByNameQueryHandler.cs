using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Room;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

public class FindRoomByNameQueryHandler : ISingleQueryHandler<FindRoomByNameQuery, Room?>
{
    public Task<Room?> Handle(EntityQuery<FindRoomByNameQuery, Room?> request, CancellationToken cancellationToken)
    {
        var name = request.Query.Name;

        return request.Context.Rooms.FirstOrDefaultAsync(predicate: t => t.Name == name, cancellationToken);
    }
}
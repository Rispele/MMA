using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Abstractions;
using Rooms.Domain.Queries.Implementations.Rooms;

namespace Rooms.Persistence.Queries.Room;

public class FindRoomByNameQuery : IFindRoomByNameQuery<RoomsDbContext>
{
    public required string Name { get; init; }

    public Task<Domain.Models.RoomModels.Room?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        return source.Rooms.FirstOrDefaultAsync(t => t.Name == Name, cancellationToken);
    }
}
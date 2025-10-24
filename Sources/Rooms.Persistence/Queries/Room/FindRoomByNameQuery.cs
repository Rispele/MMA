using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Implementations.Room;

namespace Rooms.Persistence.Queries.Room;

public class FindRoomByNameQuery : IFindRoomByNameQuery
{
    public required string Name { get; init; }

    public Task<Domain.Models.Room.Room?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        return source.Rooms.FirstOrDefaultAsync(t => t.Name == Name, cancellationToken);
    }
}
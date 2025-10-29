using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.Room;

public class FindRoomByNameQuery :
    IFindRoomByNameQuery,
    ISingleQueryImplementer<Domain.Models.Room.Room?, RoomsDbContext>
{
    public required string Name { get; init; }

    public Task<Domain.Models.Room.Room?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        return source.Rooms.FirstOrDefaultAsync(predicate: t => t.Name == Name, cancellationToken);
    }
}
using Commons.Queries.Abstractions;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Persistence;

namespace Rooms.Core.Implementations.Persistence;

public readonly struct FindRoomByIdQuery(int roomId) : ISingleQueryObject<Room?, RoomsDbContext>
{
    public Task<Room?> Apply(RoomsDbContext dbContext, CancellationToken cancellationToken)
    {
        var id = roomId;

        return dbContext.Rooms.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }
}
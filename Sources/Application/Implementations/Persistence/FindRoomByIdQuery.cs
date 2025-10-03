using Commons.Queries.Abstractions;
using Domain.Models.Room;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations.Persistence;

public readonly struct FindRoomByIdQuery(int roomId) : ISingleQueryObject<Room?, DomainDbContext>
{
    public Task<Room?> Apply(DomainDbContext dbContext, CancellationToken cancellationToken)
    {
        var id = roomId;
        
        return dbContext.Rooms.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }
}
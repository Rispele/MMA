using Domain.Models.Room;
using Domain.Persistence.Queries.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence.Queries;

public readonly struct FindRoomByIdQuery(int roomId) : ISingleQueryObject<Room?, DomainDbContext>
{
    public Task<Room?> Apply(DomainDbContext dbContext)
    {
        var id = roomId;
        
        return dbContext.Rooms.FirstOrDefaultAsync(t => t.Id == id);
    }
}
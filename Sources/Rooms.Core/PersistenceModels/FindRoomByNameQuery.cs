using Commons.Queries.Abstractions;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Room;
using Rooms.Domain.Persistence;

namespace Rooms.Core.PersistenceModels;

public class FindRoomByNameQuery(string name) : ISingleQueryObject<Room?, RoomsDbContext>
{
    public Task<Room?> Apply(RoomsDbContext dbContext, CancellationToken cancellationToken)
    {
        return dbContext.Rooms.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
    }
}
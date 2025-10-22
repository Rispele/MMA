using Commons.Queries.Abstractions;
using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Equipment;
using Rooms.Domain.Persistence;

namespace Rooms.Core.PersistenceModels;

public readonly struct FindEquipmentByIdQuery(int equipmentId) : ISingleQueryObject<Equipment?, RoomsDbContext>
{
    public Task<Equipment?> Apply(RoomsDbContext dbContext, CancellationToken cancellationToken)
    {
        var id = equipmentId;

        return dbContext.Equipments.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }
}
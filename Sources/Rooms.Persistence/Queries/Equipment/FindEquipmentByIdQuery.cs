using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Implementations.Equipments;

namespace Rooms.Persistence.Queries.Equipment;

public readonly struct FindEquipmentByIdQuery : IFindEquipmentByIdQuery<RoomsDbContext>
{
    public required int EquipmentId { get; init; }

    public Task<Domain.Models.EquipmentModels.Equipment?> Apply(RoomsDbContext source, CancellationToken cancellationToken)
    {
        var id = EquipmentId;

        return source.Equipments.FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);
    }
}
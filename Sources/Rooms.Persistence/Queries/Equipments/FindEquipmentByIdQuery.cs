using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Models.Equipment;
using Rooms.Domain.Queries.Implementations.Equipment;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.Equipments;

public readonly struct FindEquipmentByIdQuery :
    IFindEquipmentByIdQuery,
    ISingleQueryImplementer<Equipment?, RoomsDbContext>
{
    public required int EquipmentId { get; init; }

    public Task<Equipment?> Apply(RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = EquipmentId;

        return source.Equipments.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
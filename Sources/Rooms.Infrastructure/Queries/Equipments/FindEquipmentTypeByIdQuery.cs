using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.Equipments;

public readonly struct FindEquipmentTypeByIdQuery :
    IFindEquipmentTypeByIdQuery,
    ISingleQueryImplementer<EquipmentType?, RoomsDbContext>
{
    public required int EquipmentTypeId { get; init; }

    public Task<EquipmentType?> Apply(
        RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = EquipmentTypeId;

        return source.EquipmentTypes.FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
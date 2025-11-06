using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.Equipments;

public readonly struct FindEquipmentSchemaByIdQuery :
    IFindEquipmentSchemaByIdQuery,
    ISingleQueryImplementer<EquipmentSchema?, RoomsDbContext>
{
    public required int EquipmentSchemaId { get; init; }

    public Task<EquipmentSchema?> Apply(
        RoomsDbContext source,
        CancellationToken cancellationToken)
    {
        var id = EquipmentSchemaId;

        return source.EquipmentSchemas.Include(x => x.Type).FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.QueryHandlers.Equipments;

public class FindEquipmentSchemaByIdQueryHandler : ISingleQueryHandler<FindEquipmentSchemaByIdQuery, EquipmentSchema?>
{
    public Task<EquipmentSchema?> Handle(EntityQuery<FindEquipmentSchemaByIdQuery, EquipmentSchema?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.EquipmentSchemaId;

        return request.Context.EquipmentSchemas
            .Include(x => x.Type)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
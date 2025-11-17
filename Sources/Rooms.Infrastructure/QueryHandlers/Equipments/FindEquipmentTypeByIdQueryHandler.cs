using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.QueryHandlers.Equipments;

public class FindEquipmentTypeByIdQueryHandler : ISingleQueryHandler<FindEquipmentTypeByIdQuery, EquipmentType?>
{
    public Task<EquipmentType?> Handle(EntityQuery<FindEquipmentTypeByIdQuery, EquipmentType?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.EquipmentTypeId;

        return request.Context.EquipmentTypes
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
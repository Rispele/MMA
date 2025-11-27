using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

internal class FindEquipmentTypeByIdQueryHandler : ISingleQueryHandler<RoomsDbContext, FindEquipmentTypeByIdQuery, EquipmentType?>
{
    public Task<EquipmentType?> Handle(EntityQuery<RoomsDbContext, FindEquipmentTypeByIdQuery, EquipmentType?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.EquipmentTypeId;

        return request.Context.EquipmentTypes
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
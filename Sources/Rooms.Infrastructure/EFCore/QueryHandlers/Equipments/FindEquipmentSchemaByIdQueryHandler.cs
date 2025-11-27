using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

public class FindEquipmentSchemaByIdQueryHandler : ISingleQueryHandler<RoomsDbContext, FindEquipmentSchemaByIdQuery, EquipmentSchema?>
{
    public Task<EquipmentSchema?> Handle(EntityQuery<RoomsDbContext, FindEquipmentSchemaByIdQuery, EquipmentSchema?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.EquipmentSchemaId;

        return request.Context.EquipmentSchemas
            .Include(x => x.Type)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
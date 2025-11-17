using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.QueryHandlers.Equipments;

public class FindEquipmentByIdQueryHandler : ISingleQueryHandler<FindEquipmentByIdQuery, Equipment?>
{
    public Task<Equipment?> Handle(EntityQuery<FindEquipmentByIdQuery, Equipment?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.EquipmentId;

        return request.Context.Equipments
            .Include(x => x.Schema)
            .ThenInclude(x => x.Type)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
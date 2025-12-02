using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

internal class FindEquipmentByIdQueryHandler : ISingleQueryHandler<RoomsDbContext, FindEquipmentByIdQuery, Equipment?>
{
    public Task<Equipment?> Handle(EntityQuery<RoomsDbContext, FindEquipmentByIdQuery, Equipment?> request, CancellationToken cancellationToken)
    {
        var id = request.Query.EquipmentId;

        return request.Context.Equipments
            // .Include(x => x.Schema)
            // .ThenInclude(x => x.Type)
            .FirstOrDefaultAsync(predicate: t => t.Id == id, cancellationToken);
    }
}
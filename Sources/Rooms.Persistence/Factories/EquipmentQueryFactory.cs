using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Persistence.Queries.Equipments;

namespace Rooms.Persistence.Factories;

public class EquipmentQueryFactory : IEquipmentQueryFactory
{
    public IFilterEquipmentsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentId = -1,
        EquipmentsFilterDto? filter = null)
    {
        return new FilterEquipmentsQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterEquipmentId = afterEquipmentId,
            Filter = filter
        };
    }

    public IFindEquipmentByIdQuery FindById(int equipmentId)
    {
        return new FindEquipmentByIdQuery
        {
            EquipmentId = equipmentId
        };
    }
}
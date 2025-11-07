using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Infrastructure.Queries.Equipments;

namespace Rooms.Infrastructure.Factories;

public class EquipmentTypeQueryFactory : IEquipmentTypeQueryFactory
{
    public IFilterEquipmentTypesQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentTypeId = -1,
        EquipmentTypesFilterDto? filter = null)
    {
        return new FilterEquipmentTypesQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterEquipmentTypeId = afterEquipmentTypeId,
            Filter = filter
        };
    }

    public IFindEquipmentTypeByIdQuery FindById(int equipmentTypeId)
    {
        return new FindEquipmentTypeByIdQuery
        {
            EquipmentTypeId = equipmentTypeId
        };
    }
}
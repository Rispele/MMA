using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Queries.Implementations.Equipment;

namespace Rooms.Core.Queries.Factories;

public interface IEquipmentTypeQueryFactory
{
    public IFilterEquipmentTypesQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentTypeId = -1,
        EquipmentTypesFilterDto? filter = null);

    public IFindEquipmentTypeByIdQuery FindById(int equipmentTypeId);
}
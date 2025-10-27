using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Queries.Implementations.Equipment;

namespace Rooms.Core.Queries.Factories;

public interface IEquipmentQueryFactory
{
    public IFilterEquipmentsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentId = -1,
        EquipmentsFilterDto? filter = null);

    public IFindEquipmentByIdQuery FindById(int equipmentId);
}
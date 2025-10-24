using Rooms.Domain.Queries.Implementations.Equipment;

namespace Rooms.Domain.Queries.Factories;

public interface IEquipmentQueryFactory
{
    public IFilterEquipmentsQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentId = -1,
        EquipmentsFilter? filter = null);

    public IFindEquipmentByIdQuery FindById(int equipmentId);
}
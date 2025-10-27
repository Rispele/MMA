using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Queries.Implementations.Equipment;

namespace Rooms.Core.Queries.Factories;

public interface IEquipmentSchemaQueryFactory
{
    public IFilterEquipmentSchemasQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentSchemaId = -1,
        EquipmentSchemasFilterDto? filter = null);

    public IFindEquipmentSchemaByIdQuery FindById(int equipmentSchemaId);
}
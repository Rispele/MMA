using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Persistence.Queries.Equipments;

namespace Rooms.Persistence.Factories;

public class EquipmentSchemaQueryFactory : IEquipmentSchemaQueryFactory
{
    public IFilterEquipmentSchemasQuery Filter(
        int batchSize,
        int batchNumber,
        int afterEquipmentSchemaId = -1,
        EquipmentSchemasFilterDto? filter = null)
    {
        return new FilterEquipmentSchemasQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterEquipmentSchemaId = afterEquipmentSchemaId,
            Filter = filter
        };
    }

    public IFindEquipmentSchemaByIdQuery FindById(int equipmentSchemaId)
    {
        return new FindEquipmentSchemaByIdQuery
        {
            EquipmentSchemaId = equipmentSchemaId
        };
    }
}
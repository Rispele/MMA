using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Equipment.Responses;

namespace Rooms.Core.Services.Equipments.Interfaces;

public interface IEquipmentSchemaService
{
    Task<EquipmentSchemaDto> GetEquipmentSchemaById(int equipmentSchemaId, CancellationToken cancellationToken);
    Task<EquipmentSchemasResponseDto> FilterEquipmentSchemas(GetEquipmentSchemasDto dto, CancellationToken cancellationToken);
    Task<EquipmentSchemaDto> CreateEquipmentSchema(CreateEquipmentSchemaDto dto, CancellationToken cancellationToken);
    Task<EquipmentSchemaDto> PatchEquipmentSchema(int equipmentSchemaId, PatchEquipmentSchemaDto dto, CancellationToken cancellationToken);
}
using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Interfaces;

public interface IEquipmentSchemaService
{
    Task<EquipmentSchemaDto> GetEquipmentSchemaById(int equipmentSchemaId, CancellationToken cancellationToken);
    Task<EquipmentSchemasResponseDto> FilterEquipmentSchemas(GetEquipmentSchemasDto dto, CancellationToken cancellationToken);
    Task<EquipmentSchemaDto> CreateEquipmentSchema(CreateEquipmentSchemaDto dto, CancellationToken cancellationToken);
    Task<EquipmentSchemaDto> PatchEquipmentSchema(int equipmentSchemaId, PatchEquipmentSchemaDto dto, CancellationToken cancellationToken);
    Task<FileExportDto> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken);
}
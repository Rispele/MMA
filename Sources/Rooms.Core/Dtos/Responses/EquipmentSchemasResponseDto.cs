using Rooms.Core.Dtos.Equipment;

namespace Rooms.Core.Dtos.Responses;

public record EquipmentSchemasResponseDto(EquipmentSchemaDto[] EquipmentSchemas, int Count, int? LastEquipmentSchemaId);
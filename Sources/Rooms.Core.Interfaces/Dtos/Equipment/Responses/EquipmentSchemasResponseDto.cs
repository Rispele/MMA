namespace Rooms.Core.Interfaces.Dtos.Equipment.Responses;

public record EquipmentSchemasResponseDto(EquipmentSchemaDto[] EquipmentSchemas, int Count, int? LastEquipmentSchemaId);
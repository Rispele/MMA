namespace Rooms.Core.Dtos.Equipment.Responses;

public record EquipmentSchemasResponseDto(EquipmentSchemaDto[] EquipmentSchemas, int Count, int? LastEquipmentSchemaId);
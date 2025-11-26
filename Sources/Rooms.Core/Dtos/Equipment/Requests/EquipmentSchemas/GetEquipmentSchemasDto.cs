namespace Rooms.Core.Dtos.Equipment.Requests.EquipmentSchemas;

public record GetEquipmentSchemasDto(int BatchNumber, int BatchSize, int AfterEquipmentSchemaId, EquipmentSchemasFilterDto? Filter);
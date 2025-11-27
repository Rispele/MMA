using Commons.Domain.Queries.Abstractions;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public sealed record FilterEquipmentSchemasQuery(
    int BatchSize,
    int BatchNumber,
    int AfterEquipmentSchemaId,
    EquipmentSchemasFilterDto? Filter) : IQuerySpecification<FilterEquipmentSchemasQuery, EquipmentSchema>;
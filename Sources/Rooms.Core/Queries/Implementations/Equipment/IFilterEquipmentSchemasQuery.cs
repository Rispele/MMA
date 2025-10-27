using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFilterEquipmentSchemasQuery : IQuerySpecification<Domain.Models.Equipment.EquipmentSchema>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentSchemaId { get; init; }
    public EquipmentSchemasFilterDto? Filter { get; init; }
}
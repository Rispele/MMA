using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFilterEquipmentTypesQuery : IQuerySpecification<Domain.Models.Equipment.EquipmentType>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentTypeId { get; init; }
    public EquipmentTypesFilterDto? Filter { get; init; }
}
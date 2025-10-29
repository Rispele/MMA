using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFilterEquipmentTypesQuery : IQuerySpecification<EquipmentType>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentTypeId { get; init; }
    public EquipmentTypesFilterDto? Filter { get; init; }
}
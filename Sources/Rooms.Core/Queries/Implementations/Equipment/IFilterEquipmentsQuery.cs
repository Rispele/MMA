using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Equipment;

public interface IFilterEquipmentsQuery : IQuerySpecification<Models.Equipment.Equipment>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentId { get; init; }
    public EquipmentsFilterDto? Filter { get; init; }
}
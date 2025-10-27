using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFilterEquipmentsQuery : IQuerySpecification<Domain.Models.Equipment.Equipment>
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentId { get; init; }
    public EquipmentsFilterDto? Filter { get; init; }
}
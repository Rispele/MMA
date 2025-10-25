using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Equipment;

public interface IFindEquipmentByIdQuery : ISingleQuerySpecification<Models.Equipment.Equipment>
{
    public int EquipmentId { get; init; }
}
using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentByIdQuery : ISingleQuerySpecification<Domain.Models.Equipment.Equipment>
{
    public int EquipmentId { get; init; }
}
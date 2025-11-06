using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentByIdQuery : ISingleQuerySpecification<Domain.Models.Equipments.Equipment>
{
    public int EquipmentId { get; init; }
}
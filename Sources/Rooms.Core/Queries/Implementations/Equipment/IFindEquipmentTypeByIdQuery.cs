using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentTypeByIdQuery : ISingleQuerySpecification<Domain.Models.Equipment.EquipmentType>
{
    public int EquipmentTypeId { get; init; }
}
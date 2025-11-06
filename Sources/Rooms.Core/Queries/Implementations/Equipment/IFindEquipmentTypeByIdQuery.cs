using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentTypeByIdQuery : ISingleQuerySpecification<EquipmentType>
{
    public int EquipmentTypeId { get; init; }
}
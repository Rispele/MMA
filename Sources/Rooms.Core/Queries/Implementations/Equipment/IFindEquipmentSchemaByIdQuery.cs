using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentSchemaByIdQuery : ISingleQuerySpecification<EquipmentSchema>
{
    public int EquipmentSchemaId { get; init; }
}
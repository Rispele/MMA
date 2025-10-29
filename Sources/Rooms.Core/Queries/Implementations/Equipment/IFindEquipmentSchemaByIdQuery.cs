using Rooms.Core.Queries.Abstractions;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentSchemaByIdQuery : ISingleQuerySpecification<EquipmentSchema>
{
    public int EquipmentSchemaId { get; init; }
}
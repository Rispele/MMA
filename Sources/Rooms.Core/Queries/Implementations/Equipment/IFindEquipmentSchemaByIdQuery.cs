using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Implementations.Equipment;

public interface IFindEquipmentSchemaByIdQuery : ISingleQuerySpecification<Domain.Models.Equipment.EquipmentSchema>
{
    public int EquipmentSchemaId { get; init; }
}
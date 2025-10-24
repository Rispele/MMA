using Rooms.Domain.Models.EquipmentModels;
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Equipments;

public interface IFindEquipmentByIdQuery<in TSource> : ISingleQueryObject<Equipment?, TSource>
    where TSource : IModelsSource
{
    public int EquipmentId { get; init; }
}
using Rooms.Domain.Models.EquipmentModels;
using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Equipments;

public interface IFilterEquipmentsQuery<in TSource> : IQueryObject<Equipment, TSource> 
    where TSource : IModelsSource
{
    public int BatchSize { get; init; }
    public int BatchNumber { get; init; }
    public int AfterEquipmentId { get; init; }
    public EquipmentsFilter? Filter { get; init; }
}
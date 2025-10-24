using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Equipments;

public interface IEquipmentQueryFactory<in TSource> 
    where TSource : IModelsSource
{
    public IFilterEquipmentsQuery<TSource> Filter(
        int batchSize, 
        int batchNumber,
        int afterEquipmentId = -1,
        EquipmentsFilter? filter = null);
}
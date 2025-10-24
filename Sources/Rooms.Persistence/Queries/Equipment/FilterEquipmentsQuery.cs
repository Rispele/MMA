using System.Linq.Expressions;
using Commons.Optional;
using Rooms.Domain.Queries.Implementations.Equipments;
using Rooms.Domain.Queries.Implementations.Filtering;

namespace Rooms.Persistence.Queries.Equipment;

public class FilterEquipmentsQuery : IFilterEquipmentsQuery<RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterEquipmentId { get; init; } = 0;
    public EquipmentsFilter? Filter { get; init; } = null;
    
    public IAsyncEnumerable<Domain.Models.EquipmentModels.Equipment> Apply(RoomsDbContext source)
    {
        IQueryable<Domain.Models.EquipmentModels.Equipment> equipments = source.Equipments;

        equipments = Filters(equipments);
        equipments = Sort(equipments);
        equipments = Paginate(equipments);

        return equipments.ToAsyncEnumerable();
    }

    private IQueryable<Domain.Models.EquipmentModels.Equipment> Filters(IQueryable<Domain.Models.EquipmentModels.Equipment> equipments)
    {
        if (Filter is null)
        {
            return equipments;
        }

        equipments = Filter.RoomName
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                return queryable.Where(t => parameter.Values.Contains(t.Room.Name));
            });

        equipments = Filter.Types
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                return queryable.Where(t => Enumerable.Contains(parameter.Values, t.Schema.Type));
            });

        equipments = Filter.Schemas
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                return queryable.Where(t => Enumerable.Contains(parameter.Values, t.Schema));
            });

        equipments = Filter.InventoryNumber
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.InventoryNumber != null && t.InventoryNumber.Contains(parameter.Value)));

        equipments = Filter.SerialNumber
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.SerialNumber != null && t.SerialNumber.Contains(parameter.Value)));

        equipments = Filter.NetworkEquipmentIp
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.NetworkEquipmentIp != null && t.NetworkEquipmentIp.Contains(parameter.Value)));

        equipments = Filter.Comment
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.Comment != null && t.Comment.Contains(parameter.Value)));

        equipments = Filter.Statuses
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                return queryable.Where(t => parameter.Values.Any(x => x == t.Status));
            });

        return equipments;
    }

    private IQueryable<Domain.Models.EquipmentModels.Equipment> Sort(IQueryable<Domain.Models.EquipmentModels.Equipment> equipments)
    {
        if (Filter is null)
        {
            return equipments;
        }

        (SortDirection? direction, Expression<Func<Domain.Models.EquipmentModels.Equipment, object>> parameter)[] sorts =
        [
            BuildSort(Filter.RoomName?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.Types?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.Schemas?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.InventoryNumber?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.SerialNumber?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.NetworkEquipmentIp?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.Comment?.SortDirection, t => t.Room.Name),
            BuildSort(Filter.Statuses?.SortDirection, t => t.Room.Name),
        ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirection.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return equipments;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirection.Ascending => equipments.OrderBy(firstSort.parameter),
            SortDirection.Descending => equipments.OrderByDescending(firstSort.parameter),
            _ => throw new ArgumentOutOfRangeException()
        };

        foreach (var (direction, parameter) in sortsToApply.Skip(1))
        {
            orderedQueryable = direction switch
            {
                SortDirection.Ascending => orderedQueryable.ThenBy(parameter),
                SortDirection.Descending => orderedQueryable.ThenByDescending(parameter),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return orderedQueryable;

        (SortDirection? direction, Expression<Func<Domain.Models.EquipmentModels.Equipment, object>>) BuildSort(SortDirection? direction, Expression<Func<Domain.Models.EquipmentModels.Equipment, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Domain.Models.EquipmentModels.Equipment> Paginate(IQueryable<Domain.Models.EquipmentModels.Equipment> equipments)
    {
        return equipments.Where(t => t.Id > AfterEquipmentId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
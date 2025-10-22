using System.Linq.Expressions;
using Commons.Optional;
using Commons.Queries.Abstractions;
using Rooms.Core.Implementations.Dtos.Requests.EquipmentQuerying;
using Rooms.Core.Implementations.Dtos.Requests.Filtering;
using Rooms.Core.Implementations.Services.DtoConverters;
using Rooms.Domain.Models.Equipment;
using Rooms.Domain.Persistence;

namespace Rooms.Core.Implementations.Persistence;

/// <summary>
/// 
/// </summary>
/// <param name="batchNumber">from 0 to +inf</param>
public class FilterEquipmentsQuery(int batchSize, int batchNumber, int afterEquipmentId, EquipmentsFilterDto? filter) : IQueryObject<Equipment, RoomsDbContext>
{
    public IAsyncEnumerable<Equipment> Apply(RoomsDbContext dbContext)
    {
        IQueryable<Equipment> equipments = dbContext.Equipments;

        equipments = Filters(equipments);
        equipments = Sort(equipments);
        equipments = Paginate(equipments);

        return equipments.ToAsyncEnumerable();
    }

    private IQueryable<Equipment> Filters(IQueryable<Equipment> equipments)
    {
        if (filter is null)
        {
            return equipments;
        }

        equipments = filter.RoomName
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                return queryable.Where(t => parameter.Values.Contains(t.Room.Name));
            });

        equipments = filter.Types
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                var types = parameter.Values.Select(EquipmentDtoConverter.Convert).ToArray();
                return queryable.Where(t => types.Contains(t.Schema.Type));
            });

        equipments = filter.Schemas
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                var types = parameter.Values.Select(EquipmentDtoConverter.Convert).ToArray();
                return queryable.Where(t => types.Contains(t.Schema));
            });

        equipments = filter.InventoryNumber
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.InventoryNumber != null && t.InventoryNumber.Contains(parameter.Value)));

        equipments = filter.SerialNumber
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.SerialNumber != null && t.SerialNumber.Contains(parameter.Value)));

        equipments = filter.NetworkEquipmentIp
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.NetworkEquipmentIp != null && t.NetworkEquipmentIp.Contains(parameter.Value)));

        equipments = filter.Comment
            .AsOptional()
            .Apply(equipments, (queryable, parameter) => queryable.Where(t => t.Comment != null && t.Comment.Contains(parameter.Value)));

        equipments = filter.Statuses
            .AsOptional()
            .Apply(equipments, (queryable, parameter) =>
            {
                return queryable.Where(t => parameter.Values.Any(x => x == t.Status));
            });

        return equipments;
    }

    private IQueryable<Equipment> Sort(IQueryable<Equipment> equipments)
    {
        if (filter is null)
        {
            return equipments;
        }

        (SortDirectionDto? direction, Expression<Func<Equipment, object>> parameter)[] sorts =
        [
            BuildSort(filter.RoomName?.SortDirection, t => t.Room.Name),
            BuildSort(filter.Types?.SortDirection, t => t.Room.Name),
            BuildSort(filter.Schemas?.SortDirection, t => t.Room.Name),
            BuildSort(filter.InventoryNumber?.SortDirection, t => t.Room.Name),
            BuildSort(filter.SerialNumber?.SortDirection, t => t.Room.Name),
            BuildSort(filter.NetworkEquipmentIp?.SortDirection, t => t.Room.Name),
            BuildSort(filter.Comment?.SortDirection, t => t.Room.Name),
            BuildSort(filter.Statuses?.SortDirection, t => t.Room.Name),
        ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return equipments;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => equipments.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => equipments.OrderByDescending(firstSort.parameter),
            _ => throw new ArgumentOutOfRangeException()
        };

        foreach (var (direction, parameter) in sortsToApply.Skip(1))
        {
            orderedQueryable = direction switch
            {
                SortDirectionDto.Ascending => orderedQueryable.ThenBy(parameter),
                SortDirectionDto.Descending => orderedQueryable.ThenByDescending(parameter),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return orderedQueryable;

        (SortDirectionDto? direction, Expression<Func<Equipment, object>>) BuildSort(SortDirectionDto? direction, Expression<Func<Equipment, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Equipment> Paginate(IQueryable<Equipment> equipments)
    {
        return equipments.Where(t => t.Id > afterEquipmentId).Skip(batchSize * batchNumber).Take(batchSize);
    }
}
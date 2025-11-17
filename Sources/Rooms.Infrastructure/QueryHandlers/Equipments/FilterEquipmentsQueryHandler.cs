using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.QueryHandlers.Equipments;

public class FilterEquipmentsQueryHandler  : IQueryHandler<FilterEquipmentsQuery, Equipment>
{
    public Task<IAsyncEnumerable<Equipment>> Handle(EntityQuery<FilterEquipmentsQuery, IAsyncEnumerable<Equipment>> request, CancellationToken cancellationToken)
    {
        IQueryable<Equipment> equipments = request.Context.Equipments
            .Include(x => x.Schema)
            .ThenInclude(x => x.Type);

        equipments = Filters(equipments, request.Query.Filter);
        equipments = Sort(equipments, request.Query.Filter);
        equipments = Paginate(equipments, request.Query);

        return Task.FromResult(equipments.AsAsyncEnumerable());
    }

    private IQueryable<Equipment> Filters(IQueryable<Equipment> equipments, EquipmentsFilterDto? filter)
    {
        if (filter is null)
        {
            return equipments;
        }

        // equipments = Filter.RoomName
            // .AsOptional()
            // .Apply(equipments,
                // apply: (queryable, parameter) => { return queryable.Where(t => parameter.Values.Contains(t.Room.Name)); });

        // equipments = Filter.Types
            // .AsOptional()
            // .Apply(equipments,
                // apply: (queryable, parameter) =>
                // {
                    // var types = parameter.Values.Select(EquipmentTypeDtoMapper.MapEquipmentTypeFromDto);
                    // return queryable.Where(t => types.Contains(t.Schema.Type));
                // });

        // equipments = Filter.Schemas
            // .AsOptional()
            // .Apply(equipments,
                // apply: (queryable, parameter) =>
                // {
                    // var types = parameter.Values.Select(EquipmentSchemaDtoMapper.MapEquipmentSchemaFromDto);
                    // return queryable.Where(t => types.Contains(t.Schema));
                // });

        equipments = filter.InventoryNumber
            .AsOptional()
            .Apply(equipments,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.InventoryNumber != null && t.InventoryNumber.Contains(parameter.Value)));

        equipments = filter.SerialNumber
            .AsOptional()
            .Apply(equipments,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.SerialNumber != null && t.SerialNumber.Contains(parameter.Value)));

        equipments = filter.NetworkEquipmentIp
            .AsOptional()
            .Apply(equipments,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.NetworkEquipmentIp != null && t.NetworkEquipmentIp.Contains(parameter.Value)));

        equipments = filter.Comment
            .AsOptional()
            .Apply(equipments,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.Comment != null && t.Comment.Contains(parameter.Value)));

        equipments = filter.Statuses
            .AsOptional()
            .Apply(equipments,
                apply: (queryable, parameter) => { return queryable.Where(t => parameter.Values.Any(x => x == t.Status)); });

        return equipments;
    }

    private IQueryable<Equipment> Sort(IQueryable<Equipment> equipments, EquipmentsFilterDto? filter)
    {
        if (filter is null)
        {
            return equipments;
        }

        (SortDirectionDto? direction, Expression<Func<Equipment, object?>> parameter)[]
            sorts =
            [
                // BuildSort(Filter.RoomName?.SortDirection, parameter: t => t.Room.Name),
                // BuildSort(Filter.Types?.SortDirection, parameter: t => t.Room.Name),
                // BuildSort(Filter.Schemas?.SortDirection, parameter: t => t.Room.Name),
                BuildSort(filter.InventoryNumber?.SortDirection, parameter: t => t.InventoryNumber),
                BuildSort(filter.SerialNumber?.SortDirection, parameter: t => t.SerialNumber),
                BuildSort(filter.NetworkEquipmentIp?.SortDirection, parameter: t => t.NetworkEquipmentIp),
                BuildSort(filter.Comment?.SortDirection, parameter: t => t.Comment),
                BuildSort(filter.Statuses?.SortDirection, parameter: t => t.Status)
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

        (SortDirectionDto? direction, Expression<Func<Equipment, object?>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<Equipment, object?>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Equipment> Paginate(IQueryable<Equipment> equipments, FilterEquipmentsQuery requestQuery)
    {
        return equipments
            .Where(t => t.Id > requestQuery.AfterEquipmentId)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Filtering;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

public class FilterEquipmentTypesQueryHandler : IQueryHandler<FilterEquipmentTypesQuery, EquipmentType>
{
    public Task<IAsyncEnumerable<EquipmentType>> Handle(
        EntityQuery<FilterEquipmentTypesQuery, IAsyncEnumerable<EquipmentType>> request,
        CancellationToken cancellationToken)
    {
        IQueryable<EquipmentType> equipmentTypes = request.Context.EquipmentTypes;

        equipmentTypes = Filters(equipmentTypes, request.Query.Filter);
        equipmentTypes = Sort(equipmentTypes, request.Query.Filter);
        equipmentTypes = Paginate(equipmentTypes, request.Query);

        return Task.FromResult(equipmentTypes.AsAsyncEnumerable());
    }

    private IQueryable<EquipmentType> Filters(IQueryable<EquipmentType> equipmentTypes, EquipmentTypesFilterDto? filter)
    {
        if (filter is null)
        {
            return equipmentTypes;
        }

        equipmentTypes = filter.Name
            .AsOptional()
            .Apply(equipmentTypes,
                apply: (queryable, parameter) => { return queryable.Where(t => t.Name != null! && t.Name.Contains(parameter.Value)); });

        return equipmentTypes;
    }

    private IQueryable<EquipmentType> Sort(IQueryable<EquipmentType> equipmentTypes, EquipmentTypesFilterDto? filter)
    {
        if (filter is null)
        {
            return equipmentTypes;
        }

        (SortDirectionDto? direction, Expression<Func<EquipmentType, object>> parameter)[]
            sorts =
            [
                BuildSort(filter.Name?.SortDirection, parameter: t => t.Name)
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return equipmentTypes;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => equipmentTypes.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => equipmentTypes.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<EquipmentType, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<EquipmentType, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<EquipmentType> Paginate(IQueryable<EquipmentType> equipmentTypes, FilterEquipmentTypesQuery requestQuery)
    {
        return equipmentTypes
            .Where(t => t.Id > requestQuery.AfterEquipmentTypeId)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
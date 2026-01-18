using System.Linq.Expressions;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

internal class FilterEquipmentTypesQueryHandler : IPaginatedQueryHandler<RoomsDbContext, FilterEquipmentTypesQuery, EquipmentType>
{
    public Task<(IAsyncEnumerable<EquipmentType>, int)> Handle(
        EntityQuery<RoomsDbContext, FilterEquipmentTypesQuery, (IAsyncEnumerable<EquipmentType>, int)> request,
        CancellationToken cancellationToken)
    {
        IQueryable<EquipmentType> equipmentTypes = request.Context.EquipmentTypes;

        equipmentTypes = Filters(equipmentTypes, request.Query.Filter);
        var totalCount = equipmentTypes.Count();
        equipmentTypes = Sort(equipmentTypes, request.Query.Filter);
        equipmentTypes = Paginate(equipmentTypes, request.Query);

        return Task.FromResult((equipmentTypes.AsAsyncEnumerable(), totalCount));
    }

    private IQueryable<EquipmentType> Filters(IQueryable<EquipmentType> equipmentTypes, EquipmentTypesFilterDto? filter)
    {
        if (filter is null)
        {
            return equipmentTypes;
        }

        if (filter.Name != null)
        {
            equipmentTypes = filter.Name
                .AsOptional()
                .Apply(equipmentTypes,
                    apply: (queryable, parameter) => queryable
                        .Where(t => t.Name.ToLower().Contains(parameter.Value.ToLower())));
        }

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
            .OrderBy(t => t.Id)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
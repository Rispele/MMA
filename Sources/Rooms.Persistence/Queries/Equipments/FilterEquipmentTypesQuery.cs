using System.Linq.Expressions;
using Commons.Optional;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipment;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.Equipments;

public class FilterEquipmentTypesQuery :
    IFilterEquipmentTypesQuery,
    IQueryImplementer<EquipmentType, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterEquipmentTypeId { get; init; }
    public EquipmentTypesFilterDto? Filter { get; init; }

    public IAsyncEnumerable<EquipmentType> Apply(RoomsDbContext source)
    {
        IQueryable<EquipmentType> equipmentTypes = source.EquipmentTypes;

        equipmentTypes = Filters(equipmentTypes);
        equipmentTypes = Sort(equipmentTypes);
        equipmentTypes = Paginate(equipmentTypes);

        return equipmentTypes.ToAsyncEnumerable();
    }

    private IQueryable<EquipmentType> Filters(
        IQueryable<EquipmentType> equipmentTypes)
    {
        if (Filter is null) return equipmentTypes;

        equipmentTypes = Filter.Name
            .AsOptional()
            .Apply(equipmentTypes,
                (queryable, parameter) => { return queryable.Where(t => t.Name != null! && t.Name.Contains(parameter.Value)); });

        return equipmentTypes;
    }

    private IQueryable<EquipmentType> Sort(
        IQueryable<EquipmentType> equipmentTypes)
    {
        if (Filter is null) return equipmentTypes;

        (SortDirectionDto? direction, Expression<Func<EquipmentType, object>> parameter)[]
            sorts =
            [
                BuildSort(Filter.Name?.SortDirection, t => t.Name),
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0) return equipmentTypes;

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => equipmentTypes.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => equipmentTypes.OrderByDescending(firstSort.parameter),
            _ => throw new ArgumentOutOfRangeException()
        };

        foreach (var (direction, parameter) in sortsToApply.Skip(1))
            orderedQueryable = direction switch
            {
                SortDirectionDto.Ascending => orderedQueryable.ThenBy(parameter),
                SortDirectionDto.Descending => orderedQueryable.ThenByDescending(parameter),
                _ => throw new ArgumentOutOfRangeException()
            };

        return orderedQueryable;

        (SortDirectionDto? direction, Expression<Func<EquipmentType, object>>) BuildSort(
            SortDirectionDto? direction, Expression<Func<EquipmentType, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<EquipmentType> Paginate(
        IQueryable<EquipmentType> equipmentTypes)
    {
        return equipmentTypes.Where(t => t.Id > AfterEquipmentTypeId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.EquipmentSchemas;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.Equipments;

public class FilterEquipmentSchemasQuery :
    IFilterEquipmentSchemasQuery,
    IQueryImplementer<EquipmentSchema, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterEquipmentSchemaId { get; init; }
    public EquipmentSchemasFilterDto? Filter { get; init; }

    public IAsyncEnumerable<EquipmentSchema> Apply(RoomsDbContext source)
    {
        IQueryable<EquipmentSchema> equipmentSchemas = source.EquipmentSchemas.Include(x => x.Type);

        equipmentSchemas = Filters(equipmentSchemas);
        equipmentSchemas = Sort(equipmentSchemas);
        equipmentSchemas = Paginate(equipmentSchemas);

        return equipmentSchemas.ToAsyncEnumerable();
    }

    private IQueryable<EquipmentSchema> Filters(
        IQueryable<EquipmentSchema> equipmentSchemas)
    {
        if (Filter is null)
        {
            return equipmentSchemas;
        }

        equipmentSchemas = Filter.Name
            .AsOptional()
            .Apply(equipmentSchemas,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.Name != null! && t.Name.Contains(parameter.Value)));

        equipmentSchemas = Filter.EquipmentTypeName
            .AsOptional()
            .Apply(equipmentSchemas,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.Type != null! && t.Type.ToString()!.Contains(parameter.Value)));

        equipmentSchemas = Filter.EquipmentParameters
            .AsOptional()
            .Apply(equipmentSchemas,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.ParameterValues != null! && t.ParameterValues.Keys.Any(x => x.Contains(parameter.Value))));

        return equipmentSchemas;
    }

    private IQueryable<EquipmentSchema> Sort(
        IQueryable<EquipmentSchema> equipmentSchemas)
    {
        if (Filter is null)
        {
            return equipmentSchemas;
        }

        (SortDirectionDto? direction, Expression<Func<EquipmentSchema, object>> parameter)[]
            sorts =
            [
                BuildSort(Filter.Name?.SortDirection, parameter: t => t.Name),
                BuildSort(Filter.EquipmentTypeName?.SortDirection, parameter: t => t.Name),
                BuildSort(Filter.EquipmentParameters?.SortDirection, parameter: t => t.Name)
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return equipmentSchemas;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => equipmentSchemas.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => equipmentSchemas.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<EquipmentSchema, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<EquipmentSchema, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<EquipmentSchema> Paginate(
        IQueryable<EquipmentSchema> equipmentSchemas)
    {
        return equipmentSchemas.Where(t => t.Id > AfterEquipmentSchemaId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
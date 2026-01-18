using System.Linq.Expressions;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

internal class FilterEquipmentSchemasQueryHandler : IPaginatedQueryHandler<RoomsDbContext, FilterEquipmentSchemasQuery, EquipmentSchema>
{
    public Task<(IAsyncEnumerable<EquipmentSchema>, int)> Handle(
        EntityQuery<RoomsDbContext, FilterEquipmentSchemasQuery, (IAsyncEnumerable<EquipmentSchema>, int)> request,
        CancellationToken cancellationToken)
    {
        IQueryable<EquipmentSchema> equipmentSchemas = request.Context.EquipmentSchemas.Include(x => x.Type);

        equipmentSchemas = Filters(equipmentSchemas, request.Query.Filter);
        var totalCount = equipmentSchemas.Count();
        equipmentSchemas = Sort(equipmentSchemas, request.Query.Filter);
        equipmentSchemas = Paginate(equipmentSchemas, request.Query);

        Console.WriteLine(equipmentSchemas.ToQueryString());

        return Task.FromResult((equipmentSchemas.AsAsyncEnumerable(), totalCount));
    }

    private IQueryable<EquipmentSchema> Filters(IQueryable<EquipmentSchema> equipmentSchemas, EquipmentSchemasFilterDto? filter)
    {
        if (filter is null)
        {
            return equipmentSchemas;
        }

        if (filter.Name != null)
        {
            equipmentSchemas = filter.Name
                .AsOptional()
                .Apply(equipmentSchemas,
                    apply: (queryable, parameter) => queryable
                        .Where(t => t.Name.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.EquipmentTypeName != null)
        {
            equipmentSchemas = filter.EquipmentTypeName
                .AsOptional()
                .Apply(equipmentSchemas,
                    apply: (queryable, parameter) => queryable
                        .Where(t => t.Type.Name.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.EquipmentParameters != null)
        {
            equipmentSchemas = filter.EquipmentParameters
                .AsOptional()
                .Apply(equipmentSchemas,
                    apply: (queryable, parameter) => queryable.Where(schema =>
                        EquipmentsFilterFunctions.EquipmentTypeParameterFilter(schema.Type.Id, parameter.Value)));
        }

        return equipmentSchemas;
    }

    private IQueryable<EquipmentSchema> Sort(IQueryable<EquipmentSchema> equipmentSchemas, EquipmentSchemasFilterDto? filter)
    {
        if (filter is null)
        {
            return equipmentSchemas;
        }

        (SortDirectionDto? direction, Expression<Func<EquipmentSchema, object>> parameter)[]
            sorts =
            [
                BuildSort(filter.Name?.SortDirection, parameter: t => t.Name),
                BuildSort(filter.EquipmentTypeName?.SortDirection, parameter: t => t.Name),
                BuildSort(filter.EquipmentParameters?.SortDirection, parameter: t => t.Name)
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

    private IQueryable<EquipmentSchema> Paginate(IQueryable<EquipmentSchema> equipmentSchemas, FilterEquipmentSchemasQuery query)
    {
        return equipmentSchemas
            .OrderBy(t => t.Id)
            .Skip(query.BatchSize * query.BatchNumber)
            .Take(query.BatchSize);
    }
}
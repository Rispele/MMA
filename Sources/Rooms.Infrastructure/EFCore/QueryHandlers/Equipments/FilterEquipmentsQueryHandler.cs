using System.Linq.Expressions;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Equipments;

internal class FilterEquipmentsQueryHandler : IPaginatedQueryHandler<RoomsDbContext, FilterEquipmentsQuery, Equipment>
{
    public Task<(IAsyncEnumerable<Equipment>, int)> Handle(
        EntityQuery<RoomsDbContext, FilterEquipmentsQuery, (IAsyncEnumerable<Equipment>, int)> request,
        CancellationToken cancellationToken)
    {
        IQueryable<Equipment> equipments = request.Context.Equipments
            .Include(x => x.Schema)
            .ThenInclude(x => x.Type);

        equipments = Filters(equipments, request.Query.Filter);
        var totalCount = equipments.Count();
        equipments = Sort(equipments, request.Query.Filter);
        equipments = Paginate(equipments, request.Query);

        return Task.FromResult((equipments.AsAsyncEnumerable(), totalCount));
    }

    private IQueryable<Equipment> Filters(IQueryable<Equipment> equipments, EquipmentsFilterDto? filter)
    {
        if (filter is null)
        {
            return equipments;
        }

        if (filter.Rooms != null)
        {
            equipments = filter.Rooms
                .AsOptional()
                .Apply(
                    equipments,
                    apply: (queryable, parameter) => queryable
                        .Where(t => parameter.Values.Contains(t.RoomId)));
        }

        if (filter.Types != null)
        {
            equipments = filter.Types
                .AsOptional()
                .Apply(equipments,
                    apply: (queryable, parameter) => queryable
                        .Where(t => parameter.Values.Contains(t.Schema.Type.Id)));
        }

        if (filter.Schemas != null)
        {
            equipments = filter.Schemas
                .AsOptional()
                .Apply(equipments,
                    apply: (queryable, parameter) => queryable
                        .Where(t => parameter.Values.Contains(t.Schema.Id)));
        }

        if (filter.InventoryNumber != null)
        {
            equipments = filter.InventoryNumber
                .AsOptional()
                .Apply(equipments,
                    apply: (queryable, parameter) => queryable.Where(t =>
                        t.InventoryNumber != null && t.InventoryNumber.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.SerialNumber != null)
        {
            equipments = filter.SerialNumber
                .AsOptional()
                .Apply(equipments,
                    apply: (queryable, parameter) =>
                        queryable.Where(t =>
                            t.SerialNumber != null && t.SerialNumber.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.Statuses != null)
        {
            equipments = filter.Statuses
                .AsOptional()
                .Apply(equipments,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => t.Status != null && parameter.Values.Contains(t.Status.Value)));
        }

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
                BuildSort(filter.Rooms?.SortDirection, parameter: t => t.RoomId), // todo: поменять а имя
                BuildSort(filter.Types?.SortDirection, parameter: t => t.Schema.Type.Name),
                BuildSort(filter.Schemas?.SortDirection, parameter: t => t.Schema.Name),
                BuildSort(filter.InventoryNumber?.SortDirection, parameter: t => t.InventoryNumber),
                BuildSort(filter.SerialNumber?.SortDirection, parameter: t => t.SerialNumber),
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
            .OrderBy(t => t.Id)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
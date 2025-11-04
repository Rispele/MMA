using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.OperatorRooms;
using Rooms.Core.Queries.Implementations.OperatorRoom;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.OperatorRoom;

public class FilterOperatorRoomsQuery :
    IFilterOperatorRoomsQuery,
    IQueryImplementer<Domain.Models.OperatorRoom.OperatorRoom, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterOperatorRoomId { get; init; }
    public OperatorRoomsFilterDto? Filter { get; init; }

    public IAsyncEnumerable<Domain.Models.OperatorRoom.OperatorRoom> Apply(RoomsDbContext source)
    {
        IQueryable<Domain.Models.OperatorRoom.OperatorRoom> operatorRooms = source.OperatorRooms.Include(x => x.Rooms);

        operatorRooms = Filters(operatorRooms);
        operatorRooms = Sort(operatorRooms);
        operatorRooms = Paginate(operatorRooms);

        return operatorRooms.ToAsyncEnumerable();
    }

    private IQueryable<Domain.Models.OperatorRoom.OperatorRoom> Filters(
        IQueryable<Domain.Models.OperatorRoom.OperatorRoom> operatorRooms)
    {
        if (Filter is null)
        {
            return operatorRooms;
        }

        operatorRooms = Filter.Name
            .AsOptional()
            .Apply(operatorRooms,
                apply: (queryable, parameter) => { return queryable.Where(t => t.Name != null! && t.Name.Contains(parameter.Value)); });

        return operatorRooms;
    }

    private IQueryable<Domain.Models.OperatorRoom.OperatorRoom> Sort(
        IQueryable<Domain.Models.OperatorRoom.OperatorRoom> operatorRooms)
    {
        if (Filter is null)
        {
            return operatorRooms;
        }

        (SortDirectionDto? direction, Expression<Func<Domain.Models.OperatorRoom.OperatorRoom, object>> parameter)[]
            sorts =
            [
                BuildSort(Filter.Name?.SortDirection, parameter: t => t.Name)
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return operatorRooms;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => operatorRooms.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => operatorRooms.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<Domain.Models.OperatorRoom.OperatorRoom, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<Domain.Models.OperatorRoom.OperatorRoom, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Domain.Models.OperatorRoom.OperatorRoom> Paginate(
        IQueryable<Domain.Models.OperatorRoom.OperatorRoom> operatorRooms)
    {
        return operatorRooms.Where(t => t.Id > AfterOperatorRoomId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
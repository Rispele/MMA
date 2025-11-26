using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.DtoMappers;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

public class FilterRoomsQueryHandler : IQueryHandler<FilterRoomsQuery, Room>
{
    public Task<IAsyncEnumerable<Room>> Handle(
        EntityQuery<FilterRoomsQuery, IAsyncEnumerable<Room>> request,
        CancellationToken cancellationToken)
    {
        IQueryable<Room> rooms = request.Context.Rooms.Include(room => room.Equipments);

        rooms = Filters(rooms, request.Query.Filter);
        rooms = Sort(rooms, request.Query.Filter);
        rooms = Paginate(rooms, request.Query);

        return Task.FromResult(rooms.AsAsyncEnumerable());
    }

    private IQueryable<Room> Filters(IQueryable<Room> rooms, RoomsFilterDto? filter)
    {
        if (filter is null)
        {
            return rooms;
        }

        rooms = filter.Name
            .AsOptional()
            .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.Name.Contains(parameter.Value)));

        rooms = filter.Description
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.Description != null && t.Description.Contains(parameter.Value)));

        rooms = filter.RoomTypes
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoMapper.Map);
                    return queryable.Where(t => values.Contains(t.Parameters.Type));
                });

        rooms = filter.RoomLayout
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoMapper.Map);
                    return queryable.Where(t => values.Contains(t.Parameters.Layout));
                });

        rooms = filter.Seats
            .AsOptional()
            .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.Parameters.Seats >= parameter.Value));

        rooms = filter.ComputerSeats
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t => t.Parameters.ComputerSeats >= parameter.Value));

        rooms = filter.NetTypes
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoMapper.Map);
                    return queryable.Where(t => values.Contains(t.Parameters.NetType));
                });

        rooms = filter.Conditioning
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t => t.Parameters.HasConditioning == parameter.Value));

        rooms = filter.Owner
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t => t.Owner != null && t.Owner.Contains(parameter.Value)));

        rooms = filter.RoomStatuses
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoMapper.Map);
                    return queryable.Where(t => values.Contains(t.FixInfo.Status));
                });

        rooms = filter.FixDeadline
            .AsOptional()
            .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.FixInfo.FixDeadline == parameter.Value));


        rooms = filter.Comment
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.FixInfo.Comment != null && t.FixInfo.Comment.Contains(parameter.Value)));

        return rooms;
    }

    private IQueryable<Room> Sort(IQueryable<Room> rooms, RoomsFilterDto? filter)
    {
        if (filter is null)
        {
            return rooms;
        }

        (SortDirectionDto? direction, Expression<Func<Room, object?>> parameter)[] sorts =
        [
            BuildSort(filter.Name?.SortDirection, parameter: t => t.Name),
            BuildSort(filter.Description?.SortDirection, parameter: t => t.Description),
            BuildSort(filter.RoomTypes?.SortDirection, parameter: t => t.Parameters.Type),
            BuildSort(filter.RoomLayout?.SortDirection, parameter: t => t.Parameters.Layout),
            BuildSort(filter.Seats?.SortDirection, parameter: t => t.Parameters.Seats),
            BuildSort(filter.ComputerSeats?.SortDirection, parameter: t => t.Parameters.ComputerSeats),
            BuildSort(filter.NetTypes?.SortDirection, parameter: t => t.Parameters.NetType),
            BuildSort(filter.Conditioning?.SortDirection, parameter: t => t.Parameters.HasConditioning),
            BuildSort(filter.Owner?.SortDirection, parameter: t => t.Owner),
            BuildSort(filter.RoomStatuses?.SortDirection, parameter: t => t.FixInfo.Status),
            BuildSort(filter.FixDeadline?.SortDirection, parameter: t => t.FixInfo.FixDeadline),
            BuildSort(filter.Comment?.SortDirection, parameter: t => t.FixInfo.Comment)
        ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return rooms;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => rooms.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => rooms.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<Room, object?>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<Room, object?>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Room> Paginate(IQueryable<Room> rooms, FilterRoomsQuery requestQuery)
    {
        return rooms
            .Where(t => t.Id > requestQuery.AfterRoomId)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
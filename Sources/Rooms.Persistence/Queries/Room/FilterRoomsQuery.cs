using System.Linq.Expressions;
using Commons.Optional;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.Room;

public class FilterRoomsQuery :
    IFilterRoomsQuery,
    IQueryImplementer<Domain.Models.Room.Room?, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterRoomId { get; init; }
    public RoomsFilterDto? Filter { get; init; }

    public IAsyncEnumerable<Domain.Models.Room.Room> Apply(RoomsDbContext source)
    {
        IQueryable<Domain.Models.Room.Room> rooms = source.Rooms;

        rooms = Filters(rooms);
        rooms = Sort(rooms);
        rooms = Paginate(rooms);

        return rooms.ToAsyncEnumerable();
    }

    private IQueryable<Domain.Models.Room.Room> Filters(IQueryable<Domain.Models.Room.Room> rooms)
    {
        if (Filter is null)
        {
            return rooms;
        }

        rooms = Filter.Name
            .AsOptional()
            .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.Name.Contains(parameter.Value)));

        rooms = Filter.Description
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.Description != null && t.Description.Contains(parameter.Value)));

        rooms = Filter.RoomTypes
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoConverter.Convert);
                    return queryable.Where(t => values.Contains(t.Parameters.Type));
                });

        rooms = Filter.RoomLayout
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoConverter.Convert);
                    return queryable.Where(t => values.Contains(t.Parameters.Layout));
                });

        rooms = Filter.Seats
            .AsOptional()
            .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.Parameters.Seats >= parameter.Value));

        rooms = Filter.ComputerSeats
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t => t.Parameters.ComputerSeats >= parameter.Value));

        rooms = Filter.NetTypes
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoConverter.Convert);
                    return queryable.Where(t => values.Contains(t.Parameters.NetType));
                });

        rooms = Filter.Conditioning
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t => t.Parameters.HasConditioning == parameter.Value));

        rooms = Filter.Owner
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t => t.Owner != null && t.Owner.Contains(parameter.Value)));

        rooms = Filter.RoomStatuses
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) =>
                {
                    var values = parameter.Values.Select(RoomDtoConverter.Convert);
                    return queryable.Where(t => values.Contains(t.FixInfo.Status));
                });

        rooms = Filter.FixDeadline
            .AsOptional()
            .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.FixInfo.FixDeadline == parameter.Value));


        rooms = Filter.Comment
            .AsOptional()
            .Apply(rooms,
                apply: (queryable, parameter) => queryable.Where(t =>
                    t.FixInfo.Comment != null && t.FixInfo.Comment.Contains(parameter.Value)));

        return rooms;
    }

    private IQueryable<Domain.Models.Room.Room> Sort(IQueryable<Domain.Models.Room.Room> rooms)
    {
        if (Filter is null)
        {
            return rooms;
        }

        (SortDirectionDto? direction, Expression<Func<Domain.Models.Room.Room, object?>> parameter)[] sorts =
        [
            BuildSort(Filter.Name?.SortDirection, parameter: t => t.Name),
            BuildSort(Filter.Description?.SortDirection, parameter: t => t.Description),
            BuildSort(Filter.RoomTypes?.SortDirection, parameter: t => t.Parameters.Type),
            BuildSort(Filter.RoomLayout?.SortDirection, parameter: t => t.Parameters.Layout),
            BuildSort(Filter.Seats?.SortDirection, parameter: t => t.Parameters.Seats),
            BuildSort(Filter.ComputerSeats?.SortDirection, parameter: t => t.Parameters.ComputerSeats),
            BuildSort(Filter.NetTypes?.SortDirection, parameter: t => t.Parameters.NetType),
            BuildSort(Filter.Conditioning?.SortDirection, parameter: t => t.Parameters.HasConditioning),
            BuildSort(Filter.Owner?.SortDirection, parameter: t => t.Owner),
            BuildSort(Filter.RoomStatuses?.SortDirection, parameter: t => t.FixInfo.Status),
            BuildSort(Filter.FixDeadline?.SortDirection, parameter: t => t.FixInfo.FixDeadline),
            BuildSort(Filter.Comment?.SortDirection, parameter: t => t.FixInfo.Comment)
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

        (SortDirectionDto? direction, Expression<Func<Domain.Models.Room.Room, object?>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<Domain.Models.Room.Room, object?>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Domain.Models.Room.Room> Paginate(IQueryable<Domain.Models.Room.Room> rooms)
    {
        return rooms.Where(t => t.Id > AfterRoomId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
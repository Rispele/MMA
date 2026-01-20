using System.Linq.Expressions;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.Rooms;

internal class FilterRoomsQueryHandler : IPaginatedQueryHandler<RoomsDbContext, FilterRoomsQuery, Room>
{
    public Task<(IAsyncEnumerable<Room>, int)> Handle(
        EntityQuery<RoomsDbContext, FilterRoomsQuery, (IAsyncEnumerable<Room>, int)> request,
        CancellationToken cancellationToken)
    {
        IQueryable<Room> rooms = request.Context.Rooms
            .Include(room => room.Equipments)
            .Include(room => room.OperatorDepartment);

        rooms = Filters(rooms, request.Query.Filter);
        var totalCount = rooms.Count();
        rooms = Sort(rooms, request.Query.Filter);
        rooms = Paginate(rooms, request.Query);

        return Task.FromResult((rooms.AsAsyncEnumerable(), totalCount));
    }

    private IQueryable<Room> Filters(IQueryable<Room> rooms, RoomsFilterDto? filter)
    {
        if (filter is null)
        {
            return rooms;
        }

        if (filter.Name != null)
        {
            rooms = filter.Name
                .AsOptional()
                .Apply(rooms, apply: (queryable, parameter) =>
                    queryable.Where(t => t.Name.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.Description != null)
        {
            rooms = filter.Description
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => t.Description != null && t.Description.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.RoomTypes != null)
        {
            rooms = filter.RoomTypes
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => parameter.Values.Contains(t.Parameters.Type)));
        }

        if (filter.RoomLayout != null)
        {
            rooms = filter.RoomLayout
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => parameter.Values.Contains(t.Parameters.Layout)));
        }

        if (filter.Seats != null)
        {
            rooms = filter.Seats
                .AsOptional()
                .Apply(rooms, apply: (queryable, parameter) => queryable.Where(t => t.Parameters.Seats == parameter.Value));
        }

        if (filter.ComputerSeats != null)
        {
            rooms = filter.ComputerSeats
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) => queryable.Where(t => t.Parameters.ComputerSeats == parameter.Value));
        }

        if (filter.NetTypes != null)
        {
            rooms = filter.NetTypes
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => parameter.Values.Contains(t.Parameters.NetType)));
        }

        if (filter.Conditioning != null)
        {
            rooms = filter.Conditioning
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) => queryable.Where(t => t.Parameters.HasConditioning == parameter.Value));
        }

        if (filter.Owner != null)
        {
            rooms = filter.Owner
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => t.Owner != null && t.Owner.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.RoomStatuses != null)
        {
            rooms = filter.RoomStatuses
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => parameter.Values.Contains(t.FixInfo.Status)));
        }

        if (filter.FixDeadline != null)
        {
            rooms = filter.FixDeadline
                .AsOptional()
                .Apply(rooms, apply: (queryable, parameter) => queryable
                    .Where(t => t.FixInfo.FixDeadline != null && t.FixInfo.FixDeadline.Value.Date == parameter.Value.ToUniversalTime().Date));
        }

        if (filter.Comment != null)
        {
            rooms = filter.Comment
                .AsOptional()
                .Apply(rooms,
                    apply: (queryable, parameter) =>queryable.Where(t =>
                        t.FixInfo.Comment != null && t.FixInfo.Comment.ToLower().Contains(parameter.Value.ToLower())));
        }

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
            .OrderBy(t => t.Id)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
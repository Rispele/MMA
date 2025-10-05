using System.Linq.Expressions;
using Application.Implementations.Dtos.Requests.Filtering;
using Application.Implementations.Dtos.Requests.RoomsQuerying;
using Application.Implementations.Services.DtoConverters;
using Commons.Optional;
using Commons.Queries.Abstractions;
using Domain.Models.Room;
using Domain.Persistence;

namespace Application.Implementations.Persistence;

/// <summary>
/// 
/// </summary>
/// <param name="batchNumber">from 0 to +inf</param>
public class FilterRoomsQuery(int batchSize, int batchNumber, int afterRoomId, RoomsFilterDto? filter, RoomDtoConverter roomDtoConverter) : IQueryObject<Room, DomainDbContext>
{
    public IAsyncEnumerable<Room> Apply(DomainDbContext dbContext)
    {
        IQueryable<Room> rooms = dbContext.Rooms;

        rooms = Filters(rooms);
        rooms = Sort(rooms);
        rooms = Paginate(rooms);

        return rooms.ToAsyncEnumerable();
    }

    private IQueryable<Room> Filters(IQueryable<Room> rooms)
    {
        if (filter is null)
        {
            return rooms;
        }

        rooms = filter.Name
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.Name.Contains(parameter.Value)));

        rooms = filter.Description
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.Description != null && t.Description.Contains(parameter.Value)));

        rooms = filter.RoomTypes
            .AsOptional()
            .Apply(rooms, (queryable, parameter) =>
            {
                var types = parameter.Values.Select(roomDtoConverter.Convert).ToArray();
                return queryable.Where(t => Enumerable.Contains(types, t.Parameters.Type));
            });

        rooms = filter.RoomLayout
            .AsOptional()
            .Apply(rooms, (queryable, parameter) =>
            {
                var types = parameter.Values.Select(roomDtoConverter.Convert).ToArray();
                return queryable.Where(t => Enumerable.Contains(types, t.Parameters.Layout));
            });

        rooms = filter.Seats
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.Parameters.Seats >= parameter.Value));

        rooms = filter.ComputerSeats
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.Parameters.ComputerSeats >= parameter.Value));

        rooms = filter.NetTypes
            .AsOptional()
            .Apply(rooms, (queryable, parameter) =>
            {
                var types = parameter.Values.Select(roomDtoConverter.Convert).ToArray();
                return queryable.Where(t => Enumerable.Contains(types, t.Parameters.NetType));
            });

        rooms = filter.Conditioning
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.Parameters.HasConditioning == parameter.Value));

        rooms = filter.Owner
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.Owner != null && t.Owner.Contains(parameter.Value)));

        rooms = filter.RoomStatuses
            .AsOptional()
            .Apply(rooms, (queryable, parameter) =>
            {
                var values = parameter.Values.Select(roomDtoConverter.Convert).ToArray();
                return queryable.Where(t => Enumerable.Contains(values, t.FixInfo.Status));
            });

        rooms = filter.FixDeadline
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.FixInfo.FixDeadline == parameter.Value));


        rooms = filter.Comment
            .AsOptional()
            .Apply(rooms, (queryable, parameter) => queryable.Where(t => t.FixInfo.Comment != null && t.FixInfo.Comment.Contains(parameter.Value)));

        return rooms;
    }

    private IQueryable<Room> Sort(IQueryable<Room> rooms)
    {
        if (filter is null)
        {
            return rooms;
        }

        (SortDirectionDto? direction, Expression<Func<Room, object>> parameter)[] sorts =
        [
            BuildSort(filter.Name?.SortDirectionDto, t => t.Name),
            BuildSort(filter.Description?.SortDirectionDto, t => t.Name),
            BuildSort(filter.RoomTypes?.SortDirectionDto, t => t.Name),
            BuildSort(filter.RoomLayout?.SortDirectionDto, t => t.Name),
            BuildSort(filter.Seats?.SortDirectionDto, t => t.Name),
            BuildSort(filter.ComputerSeats?.SortDirectionDto, t => t.Name),
            BuildSort(filter.NetTypes?.SortDirectionDto, t => t.Name),
            BuildSort(filter.Conditioning?.SortDirectionDto, t => t.Name),
            BuildSort(filter.Owner?.SortDirectionDto, t => t.Name),
            BuildSort(filter.RoomStatuses?.SortDirectionDto, t => t.Name),
            BuildSort(filter.FixDeadline?.SortDirectionDto, t => t.Name),
            BuildSort(filter.Comment?.SortDirectionDto, t => t.Name),
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

        (SortDirectionDto? direction, Expression<Func<Room, object>>) BuildSort(SortDirectionDto? direction, Expression<Func<Room, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Room> Paginate(IQueryable<Room> rooms)
    {
        return rooms.Where(t => t.Id > afterRoomId).Skip(batchSize * batchNumber).Take(batchSize);
    }
}
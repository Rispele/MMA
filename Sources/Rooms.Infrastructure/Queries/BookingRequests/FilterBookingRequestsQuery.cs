using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.BookingRequests;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Queries.Implementations.BookingRequest;
using Rooms.Domain.Models.BookingRequests;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.BookingRequests;

public class FilterBookingRequestsQuery :
    IFilterBookingRequestsQuery,
    IQueryImplementer<BookingRequest, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterBookingRequestId { get; init; }
    public BookingRequestsFilterDto? Filter { get; init; }

    public IAsyncEnumerable<BookingRequest> Apply(RoomsDbContext source)
    {
        IQueryable<BookingRequest> bookingRequests = source.BookingRequests;

        bookingRequests = Filters(bookingRequests);
        bookingRequests = Sort(bookingRequests);
        bookingRequests = Paginate(bookingRequests);

        return bookingRequests.AsAsyncEnumerable();
    }

    private IQueryable<BookingRequest> Filters(
        IQueryable<BookingRequest> bookingRequests)
    {
        if (Filter is null)
        {
            return bookingRequests;
        }

        bookingRequests = Filter.CreatedAt
            .AsOptional()
            .Apply(bookingRequests, apply: (queryable, parameter) => queryable.Where(t => t.CreatedAt.ToString("dd.MM.yyyy, h:mm").StartsWith(parameter.Value)));

        bookingRequests = Filter.EventName
            .AsOptional()
            .Apply(bookingRequests,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.EventName.StartsWith(parameter.Value, StringComparison.CurrentCultureIgnoreCase)));

        bookingRequests = Filter.Status
            .AsOptional()
            .Apply(bookingRequests,
                apply: (queryable, parameter) =>
                    queryable.Where(t => parameter.Values.Contains(t.Status)));

        bookingRequests = Filter.ModeratorComment
            .AsOptional()
            .Apply(bookingRequests,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.ModeratorComment != null && t.ModeratorComment.StartsWith(parameter.Value, StringComparison.CurrentCultureIgnoreCase)));

        bookingRequests = Filter.BookingScheduleStatus
            .AsOptional()
            .Apply(bookingRequests,
                apply: (queryable, parameter) =>
                    queryable.Where(t => t.BookingScheduleStatus != null && parameter.Values.Contains(t.BookingScheduleStatus.Value)));

        return bookingRequests;
    }

    private IQueryable<BookingRequest> Sort(
        IQueryable<BookingRequest> bookingRequests)
    {
        if (Filter is null)
        {
            return bookingRequests;
        }

        (SortDirectionDto? direction, Expression<Func<BookingRequest, object?>> parameter)[]
            sorts =
            [
                BuildSort(Filter.CreatedAt?.SortDirection, parameter: t => t.CreatedAt),
                BuildSort(Filter.EventName?.SortDirection, parameter: t => t.EventName),
                BuildSort(Filter.Status?.SortDirection, parameter: t => t.Status),
                BuildSort(Filter.ModeratorComment?.SortDirection, parameter: t => t.ModeratorComment),
                BuildSort(Filter.BookingScheduleStatus?.SortDirection, parameter: t => t.BookingScheduleStatus),
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return bookingRequests;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => bookingRequests.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => bookingRequests.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<BookingRequest, object?>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<BookingRequest, object?>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<BookingRequest> Paginate(
        IQueryable<BookingRequest> bookingRequests)
    {
        return bookingRequests.Where(t => t.Id > AfterBookingRequestId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
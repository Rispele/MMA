using System.Linq.Expressions;
using Booking.Core.Interfaces.Dtos.BookingRequest.Requests;
using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Models.BookingRequests;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal class FilterBookingRequestsQueryHandler : IPaginatedQueryHandler<BookingDbContext, FilterBookingRequestsQuery, BookingRequest>
{
    public Task<(IAsyncEnumerable<BookingRequest>, int)> Handle(EntityQuery<BookingDbContext, FilterBookingRequestsQuery, (IAsyncEnumerable<BookingRequest>, int)> request, CancellationToken cancellationToken)
    {
        IQueryable<BookingRequest> bookingRequests = request.Context.BookingRequests;

        bookingRequests = Filters(bookingRequests, request.Query.Filter);
        var totalCount = bookingRequests.Count();
        bookingRequests = Sort(bookingRequests, request.Query.Filter);
        bookingRequests = Paginate(bookingRequests, request.Query);

        return Task.FromResult((bookingRequests.AsAsyncEnumerable(), totalCount));
    }

    private IQueryable<BookingRequest> Filters(IQueryable<BookingRequest> bookingRequests, BookingRequestsFilterDto? filter)
    {
        if (filter is null)
        {
            return bookingRequests;
        }

        if (filter.CreatedAt != null)
        {
            bookingRequests = filter.CreatedAt
                .AsOptional()
                .Apply(bookingRequests,
                    apply: (queryable, parameter) => queryable
                        .Where(t => t.CreatedAt.Date == parameter.Value.ToUniversalTime().Date));
        }

        if (filter.EventName != null)
        {
            bookingRequests = filter.EventName
                .AsOptional()
                .Apply(bookingRequests,
                    apply: (queryable, parameter) => queryable
                        .Where(t => t.EventName.ToLower().Contains(parameter.Value.ToLower())));
        }

        if (filter.Status != null)
        {
            bookingRequests = filter.Status
                .AsOptional()
                .Apply(bookingRequests,
                    apply: (queryable, parameter) => queryable
                        .Where(t => parameter.Values.Contains(t.Status)));
        }

        if (filter.BookingScheduleStatus != null)
        {
            bookingRequests = filter.BookingScheduleStatus
                .AsOptional()
                .Apply(bookingRequests,
                    apply: (queryable, parameter) => queryable
                        .Where(t => parameter.Values.Contains(t.BookingScheduleStatus)));
        }

        if (filter.Rooms != null)
        {
            bookingRequests = filter.Rooms
                .AsOptional()
                .Apply(bookingRequests,
                    apply: (queryable, parameter) => queryable.Where(t =>
                        BookingRequestsFilterFunctions.RoomIdsParameterFilter(t.Id, parameter.Values)));
        }

        return bookingRequests;
    }

    private IQueryable<BookingRequest> Sort(IQueryable<BookingRequest> bookingRequests, BookingRequestsFilterDto? filter)
    {
        if (filter is null)
        {
            return bookingRequests;
        }

        (SortDirectionDto? direction, Expression<Func<BookingRequest, object>> parameter)[]
            sorts =
            [
                BuildSort(filter.CreatedAt?.SortDirection, parameter: t => t.CreatedAt),
                BuildSort(filter.EventName?.SortDirection, parameter: t => t.EventName),
                BuildSort(filter.Status?.SortDirection, parameter: t => t.Status)
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

        (SortDirectionDto? direction, Expression<Func<BookingRequest, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<BookingRequest, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<BookingRequest> Paginate(IQueryable<BookingRequest> bookingRequests, FilterBookingRequestsQuery requestQuery)
    {
        return bookingRequests
            .OrderBy(t => t.Id)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
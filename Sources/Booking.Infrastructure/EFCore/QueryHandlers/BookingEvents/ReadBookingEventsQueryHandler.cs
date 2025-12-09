using Booking.Core.Queries.BookingEvents;
using Booking.Domain.Events;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingEvents;

internal class ReadBookingEventsQueryHandler : IQueryHandler<BookingDbContext, ReadBookingEventsQuery, BookingEvent>
{
    public Task<IAsyncEnumerable<BookingEvent>> Handle(
        EntityQuery<BookingDbContext, ReadBookingEventsQuery, IAsyncEnumerable<BookingEvent>> request,
        CancellationToken cancellationToken)
    {
        var enumerable = request.Context.BookingEvents
            .Where(@event => @event.Id > request.Query.FromEventId)
            .OrderBy(@event => @event.Id)
            .Take(request.Query.BatchSize)
            .AsAsyncEnumerable();

        return Task.FromResult(enumerable);
    }
}
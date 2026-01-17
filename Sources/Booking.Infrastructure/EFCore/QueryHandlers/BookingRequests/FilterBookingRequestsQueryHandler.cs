using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal class FilterBookingRequestsQueryHandler : IQueryHandler<BookingDbContext, FilterBookingRequestsQuery, BookingRequest>
{
    public Task<IAsyncEnumerable<BookingRequest>> Handle(EntityQuery<BookingDbContext, FilterBookingRequestsQuery, IAsyncEnumerable<BookingRequest>> request, CancellationToken cancellationToken)
    {
        IQueryable<BookingRequest> bookingRequests = request.Context.BookingRequests;

        return Task.FromResult(bookingRequests.AsAsyncEnumerable());
    }
}
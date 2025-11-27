using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal class FilterBookingRequestsQueryHandler : IQueryHandler<BookingDbContext, FilterBookingRequestsQuery, BookingRequest>
{
    public Task<IAsyncEnumerable<BookingRequest>> Handle(EntityQuery<BookingDbContext, FilterBookingRequestsQuery, IAsyncEnumerable<BookingRequest>> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
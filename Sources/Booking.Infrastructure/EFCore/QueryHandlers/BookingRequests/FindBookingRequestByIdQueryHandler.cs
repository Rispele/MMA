using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal class FindBookingRequestByIdQueryHandler : ISingleQueryHandler<BookingDbContext, FindBookingRequestByIdQuery, BookingRequest>
{
    public Task<BookingRequest> Handle(EntityQuery<BookingDbContext, FindBookingRequestByIdQuery, BookingRequest> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal class FindBookingRequestByIdQueryHandler : ISingleQueryHandler<BookingDbContext, FindBookingRequestByIdQuery, BookingRequest?>
{
    public Task<BookingRequest?> Handle(
        EntityQuery<BookingDbContext, FindBookingRequestByIdQuery, BookingRequest?> request,
        CancellationToken cancellationToken)
    {
        return request.Context.BookingRequests.FirstOrDefaultAsync(t => t.Id == request.Query.BookingRequestId, cancellationToken: cancellationToken);
    }
}
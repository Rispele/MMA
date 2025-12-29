using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Exceptions;
using Booking.Domain.Models.BookingProcesses;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingRequests;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;

internal class
    GetBookingRequestByIdQueryHandler : ISingleQueryHandler<BookingDbContext, GetBookingRequestByIdQuery,
    BookingRequest>
{
    public async Task<BookingRequest> Handle(
        EntityQuery<BookingDbContext, GetBookingRequestByIdQuery, BookingRequest> request,
        CancellationToken cancellationToken)
    {
        var bookingRequest = await request.Context.BookingRequests
            .FirstOrDefaultAsync(
                bookingRequest => bookingRequest.Id == request.Query.BookingRequestId,
                cancellationToken: cancellationToken);

        return bookingRequest ??
               throw new BookingRequestNotFound(
                   $"Заявка с идентификатором [{request.Query.BookingRequestId}] не найдена.");
    }
}
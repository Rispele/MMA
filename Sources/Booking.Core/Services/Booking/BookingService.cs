using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingRequest;
using Commons.Domain.Queries.Factories;

namespace Booking.Core.Services.Booking;

public class BookingService(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory) : IBookingService
{
    public async Task SendBookingRequestForApprovalInEdms(int bookingRequestId, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);
        
        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        var bookingEvent = bookingRequest.SendForApprovalInEdms();
        
        unitOfWork.Add(bookingEvent);
        
        await unitOfWork.Commit(cancellationToken);
    }

    public Task SaveEdmsApprovingResult(int bookingRequestId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
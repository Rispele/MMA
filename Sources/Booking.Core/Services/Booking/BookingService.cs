using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingRequest;
using Commons.Domain.Queries.Factories;

namespace Booking.Core.Services.Booking;

public class BookingService(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory) : IBookingService
{
    public async Task InitiateBookingRequest(int bookingRequestId, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        bookingRequest.Initiate();

        await unitOfWork.Commit(cancellationToken);
    }

    public async Task SaveModerationResult(int bookingRequestId, bool isApproved, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        bookingRequest.SaveModerationResult(isApproved);

        await unitOfWork.Commit(cancellationToken);
    }
}
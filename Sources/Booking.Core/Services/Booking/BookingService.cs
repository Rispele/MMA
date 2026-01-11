using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingRequest;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Domain.Models.BookingRequests;
using Commons.Domain.Queries.Factories;
using Commons.ExternalClients.Booking;

namespace Booking.Core.Services.Booking;

public class BookingService(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IBookingClient bookingClient) : IBookingService
{
    public async Task InitiateBookingRequest(int bookingRequestId, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        bookingRequest.InitiateBookingProcess();

        await unitOfWork.Commit(cancellationToken);
    }

    public async Task SaveModerationResult(int bookingRequestId, bool isApproved, string moderatorComment, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        await ConfirmBookings(bookingRequest, cancellationToken);

        bookingRequest.SaveModerationResult(isApproved, moderatorComment);
        
        await unitOfWork.Commit(cancellationToken);
    }

    private async Task ConfirmBookings(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        var eventIds = bookingRequest.BookingProcess!
            .GetEventsOfType<BookingRequestRoomBookedForDayEventPayload>()
            .Select(t => t.Payload.GetPayload<BookingRequestRoomBookedForDayEventPayload>())
            .Select(t => t.EventId);

        foreach (var eventId in eventIds)
        {
            await bookingClient.ConfirmBooking(eventId, cancellationToken);
        }
    }
}
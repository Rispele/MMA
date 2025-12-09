using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestSentForApprovalInEdmsEventProcessor(ILogger<BookingRequestSentForApprovalInEdmsEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestSentForApprovalInEdmsEventPayload>
{
    public Type PayloadType => typeof(BookingRequestSentForApprovalInEdmsEventPayload);

    public Task ProcessEvent(IUnitOfWork _, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Sending booking request [{BookingRequestId}] for approval in edms...",
            bookingEvent.BookingRequestId);

        return Task.CompletedTask;
    }
}
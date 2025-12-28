using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestSentForApprovalInEdmsEventProcessor(ILogger<BookingRequestSentForApprovalInEdmsEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestSentForApprovalInEdmsEventPayload>
{
    public Type PayloadType => typeof(BookingRequestSentForApprovalInEdmsEventPayload);

    public Task<ProcessorResult> ProcessEvent(IUnitOfWork _, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation(
                "Sending booking request [{BookingRequestId}] for approval in edms...",
                bookingEvent.BookingRequestId);
            return Task.FromResult(new ProcessorResult(bookingEvent, ResultType.Success));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing event: [{EventId}]", bookingEvent.Id);
            return Task.FromResult(new ProcessorResult(bookingEvent, ResultType.Failure));
        }
    }

    public async Task RollbackEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        if (bookingEvent.RolledBack)
        {
            return;
        }
        
        logger.LogInformation("Rollback sending booking request for approval in edms event... BookingEvent: [{BookingEventId}]", bookingEvent.Id);
        bookingEvent.Rollback();
    }
}
using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestSentForApprovalInEdmsEventProcessor(
    ILogger<BookingRequestSentForApprovalInEdmsEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestSentForApprovalInEdmsEventPayload>
{
    public Type PayloadType => typeof(BookingRequestSentForApprovalInEdmsEventPayload);

    public Task<SynchronizeEventProcessorResult> ProcessEvent(
        IUnitOfWork _,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation(
                "Sending booking request [{BookingRequestId}] for approval in edms...",
                bookingEvent.BookingRequestId);
            return Task.FromResult(
                new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Success));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing event: [{EventId}]", bookingEvent.Id);
            return Task.FromResult(new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Retry));
        }
    }

    public async Task<RollBackEventResultType> RollbackEvent(
        IUnitOfWork unitOfWork,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        if (bookingEvent.RolledBack)
        {
            return RollBackEventResultType.RolledBack;
        }

        logger.LogInformation(
            "Rollback sending booking request for approval in edms event... BookingEvent: [{BookingEventId}]",
            bookingEvent.Id);

        try
        {
            var bookingRequest = await unitOfWork.ApplyQuery(
                new GetBookingRequestByIdQuery(bookingEvent.BookingRequestId),
                cancellationToken);

            bookingRequest.BookingProcess!.RollBackEvent(bookingEvent.Id);
            return RollBackEventResultType.RolledBack;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while rolling back event: [{EventId}]", bookingEvent.Id);
            return RollBackEventResultType.Failed;
        }
    }
}
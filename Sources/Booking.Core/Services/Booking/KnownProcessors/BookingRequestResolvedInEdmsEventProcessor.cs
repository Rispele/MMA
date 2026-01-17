using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestResolvedInEdmsEventProcessor(
    ILogger<BookingRequestResolvedInEdmsEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestResolvedInEdmsEventPayload>
{
    public Type PayloadType => typeof(BookingRequestResolvedInEdmsEventPayload);

    public async Task<SynchronizeEventProcessorResult> ProcessEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Saving edms resolution result. Booking request [{BookingRequestId}]...", bookingEvent.BookingRequestId);

            var bookingRequestId = bookingEvent.BookingRequestId;
            var payload = bookingEvent.Payload.GetPayload<BookingRequestResolvedInEdmsEventPayload>();

            var result = await SaveEdmsResolutionResult(unitOfWork, bookingRequestId, payload, cancellationToken);
            return new SynchronizeEventProcessorResult(bookingEvent, result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing edms resolution result. Booking request [{BookingRequestId}]...", bookingEvent.BookingRequestId);
            return new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Retry);
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

    private static async Task<SynchronizeEventResultType> SaveEdmsResolutionResult(
        IUnitOfWork unitOfWork,
        int bookingRequestId,
        BookingRequestResolvedInEdmsEventPayload payload,
        CancellationToken cancellationToken)
    {
        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        bookingRequest.SaveEdmsResolutionResult(payload.IsApproved, payload.ErrorMessage);
        if (!payload.IsApproved)
        {
            return SynchronizeEventResultType.Rollback;
        }

        bookingRequest.SendForModeration();

        return SynchronizeEventResultType.Success;
    }
}
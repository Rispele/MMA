using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Domain.Models.BookingRequests;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking;

public class BookingEventProcessor(
    IEnumerable<IBookingEventProcessor<IBookingEventPayload>> processors,
    ILogger<BookingEventProcessor> logger)
{
    public async Task ProcessEvent(
        IUnitOfWork unitOfWork,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        var processor = processors.FirstOrDefault(t => t.PayloadType == bookingEvent.Payload.GetType());

        if (processor is not null)
        {
            var bookingRequest = GatBookingRequestLazy(unitOfWork, bookingEvent, cancellationToken);
            try
            {
                var result = await processor.ProcessEvent(unitOfWork, bookingEvent, cancellationToken);
                await ProcessResult(bookingRequest, result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unexpected error occured while processing event: [{EventId}]", bookingEvent.Id);
                await ProcessResult(
                    bookingRequest,
                    new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Retry));
            }
        }

        logger.LogInformation("No processor found for {Name}", bookingEvent.Payload.GetType().Name);
    }

    private static async Task ProcessResult(
        Lazy<Task<BookingRequest>> bookingRequestTask,
        SynchronizeEventProcessorResult result)
    {
        if (result.Result is SynchronizeEventResultType.Skipped)
        {
            return;
        }

        var bookingRequest = await bookingRequestTask.Value;
        switch (result.Result)
        {
            case SynchronizeEventResultType.Success:
                bookingRequest.MarkEventProcessAttemptSucceeded(result.BookingEvent.Id);
                break;
            case SynchronizeEventResultType.Retry:
                bookingRequest.MarkEventProcessAttemptFailed(@result.BookingEvent.Id);
                break;
            case SynchronizeEventResultType.Rollback:
                bookingRequest.InitiateBookingProcessRollback();
                break;
            case SynchronizeEventResultType.Skipped:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static Lazy<Task<BookingRequest>> GatBookingRequestLazy(
        IUnitOfWork unitOfWork,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        return new Lazy<Task<BookingRequest>>(() =>
        {
            var request = new GetBookingRequestByIdQuery(bookingEvent.BookingRequestId);
            return unitOfWork.ApplyQuery(request, cancellationToken);
        });
    }
}
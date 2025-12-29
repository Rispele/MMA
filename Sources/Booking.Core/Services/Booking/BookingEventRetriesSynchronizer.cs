using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingProcesses;
using Booking.Core.Services.Booking.KnownProcessors;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Domain.Models.BookingRequests;
using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking;

public class BookingEventRetriesSynchronizer(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IEnumerable<IBookingEventProcessor<IBookingEventPayload>> processors,
    ILogger<BookingEventsSynchronizer> logger) : IBookingEventRetriesSynchronizer
{
    public async Task RetryFailedProcesses(int batchSize, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequests = await (await unitOfWork.ApplyQuery(
                new GetBookingRequestsWithRetryingBookingProcesses(batchSize),
                cancellationToken))
            .ToListAsync(cancellationToken);

        var processesProcessed = 0;
        foreach (var bookingRequest in bookingRequests)
        {
            await ProcessBookingRequest(bookingRequest, unitOfWork, cancellationToken);

            processesProcessed++;
        }

        await unitOfWork.Commit(cancellationToken);
        logger.LogInformation("Processed {EventsProcessed} failed processes", processesProcessed);
    }

    private async Task ProcessBookingRequest(
        BookingRequest bookingRequest,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var eventsToRetry = bookingRequest.BookingProcess!.GetEventsToRetry().ToArray();

        foreach (var eventToRetry in eventsToRetry)
        {
            var result = await ProcessEvent(unitOfWork, eventToRetry, cancellationToken);
            ProcessResult(bookingRequest, result, eventToRetry);
        }
    }

    private async Task<SynchronizeEventProcessorResult> ProcessEvent(
        IUnitOfWork unitOfWork,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        var processor = processors.FirstOrDefault(t => t.PayloadType == bookingEvent.Payload.GetType());

        if (processor is not null)
        {
            return await processor.ProcessEvent(unitOfWork, bookingEvent, cancellationToken);
        }

        logger.LogInformation("No processor found for {Name}", bookingEvent.Payload.GetType().Name);

        return new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Skipped);
    }

    private static void ProcessResult(
        BookingRequest bookingRequest,
        SynchronizeEventProcessorResult result,
        BookingEvent @event)
    {
        if (result.Result is SynchronizeEventResultType.Retry)
        {
            bookingRequest.MarkEventProcessAttemptFailed(@event.Id);
        }

        if (result.Result is SynchronizeEventResultType.Success)
        {
            bookingRequest.MarkEventProcessAttemptSucceeded(@event.Id);
        }
    }
}
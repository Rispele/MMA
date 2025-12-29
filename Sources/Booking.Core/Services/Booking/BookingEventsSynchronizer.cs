using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingEvents;
using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking;

public class BookingEventsSynchronizer(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IEnumerable<IBookingEventProcessor<IBookingEventPayload>> processors,
    ILogger<BookingEventsSynchronizer> logger) : IBookingEventsSynchronizer
{
    public async Task<int> Synchronize(
        int fromEventId,
        int batchSize,
        CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var events = await unitOfWork.ApplyQuery(new ReadBookingEventsQuery(fromEventId, batchSize), cancellationToken);

        var nextOffset = fromEventId;
        var eventsProcessed = 0;
        await foreach (var @event in events.WithCancellation(cancellationToken))
        {
            var result = await ProcessEvent(unitOfWork, @event, cancellationToken);

            await ProcessResult(unitOfWork, result, @event, cancellationToken);

            eventsProcessed++;
            nextOffset = Math.Max(nextOffset, @event.Id);
        }

        await unitOfWork.Commit(cancellationToken);
        logger.LogInformation("Processed {EventsProcessed} events", eventsProcessed);

        return nextOffset;
    }

    private async Task<ProcessorResult> ProcessEvent(
        IUnitOfWork unitOfWork,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        var processor = processors.FirstOrDefault(t => t.PayloadType == bookingEvent.Payload.GetType());

        if (processor is null)
        {
            throw new InvalidOperationException($"No processor found for {bookingEvent.Payload.GetType().Name}");
        }

        return await processor.ProcessEvent(unitOfWork, bookingEvent, cancellationToken);
    }

    private static async Task ProcessResult(
        IUnitOfWork unitOfWork,
        ProcessorResult result,
        BookingEvent @event,
        CancellationToken cancellationToken)
    {
        if (result.Result is ResultType.Retry)
        {
            var getBookingRequest = new GetBookingRequestByIdQuery(@event.BookingRequestId);
            var bookingRequest = await unitOfWork.ApplyQuery(getBookingRequest, cancellationToken);

            bookingRequest.MarkEventProcessAttemptFailed(@event.Id);
        }
    }
}
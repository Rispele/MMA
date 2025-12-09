using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingEvents;
using Booking.Core.Services.Booking.KnownProcessors;
using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
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
            await ProcessEvent(unitOfWork, @event, cancellationToken);

            eventsProcessed++;
            nextOffset = Math.Max(nextOffset, @event.Id);
        }

        logger.LogInformation("Processed {EventsProcessed} events", eventsProcessed);

        return nextOffset;
    }

    private async Task ProcessEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        var processor = processors.FirstOrDefault(t => t.PayloadType == bookingEvent.Payload.GetType());

        if (processor is null)
        {
            throw new InvalidOperationException($"No processor found for {bookingEvent.Payload.GetType().Name}");
        }

        await processor.ProcessEvent(unitOfWork, bookingEvent, cancellationToken);
    }
}
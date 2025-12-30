using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingProcesses;
using Commons;
using Commons.Domain.Queries.Factories;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking;

public class BookingEventsSynchronizer(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    BookingEventProcessor bookingEventProcessor,
    ILogger<BookingEventsSynchronizer> logger) : IBookingEventsSynchronizer
{
    public async Task<int> Synchronize(
        int fromEventId,
        int batchSize,
        CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var events =
            await (await unitOfWork.ApplyQuery(new ReadBookingEventsQuery(fromEventId, batchSize), cancellationToken))
                .ToListAsync(cancellationToken);

        var nextOffset = fromEventId;
        var eventsProcessed = 0;
        foreach (var @event in events)
        {
            await bookingEventProcessor.ProcessEvent(unitOfWork, @event, cancellationToken);

            eventsProcessed++;
            nextOffset = Math.Max(nextOffset, @event.Id);
        }

        await unitOfWork.Commit(cancellationToken);
        logger.LogInformation("Processed {EventsProcessed} events", eventsProcessed);

        return nextOffset;
    }
}
using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Queries.BookingProcesses;
using Booking.Domain.Models.BookingRequests;
using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking;

public class BookingEventRetriesSynchronizer(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    BookingEventProcessor bookingEventProcessor,
    ILogger<BookingEventsSynchronizer> logger) : IBookingEventRetriesSynchronizer
{
    public async Task RetryFailedProcesses(int batchSize, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequests = await (await unitOfWork.ApplyQuery(
                new GetBookingRequestsToRetry(batchSize),
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
            await bookingEventProcessor.ProcessEvent(unitOfWork, eventToRetry, cancellationToken);
        }
    }
}
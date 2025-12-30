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

public class BookingProcessRollbackSynchronizer(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IEnumerable<IBookingEventProcessor<IBookingEventPayload>> processors,
    ILogger<BookingProcessRollbackSynchronizer> logger) : IBookingProcessRollbackSynchronizer
{
    public async Task RollbackProcesses(int batchSize, CancellationToken cancellationToken)
    {
        await using var unitOfWork = await unitOfWorkFactory.Create(cancellationToken);

        var bookingRequests = await (await unitOfWork.ApplyQuery(
                new GetBookingRequestsToRollback(batchSize),
                cancellationToken))
            .ToListAsync(cancellationToken);

        var processedProcesses = 0;
        foreach (var bookingRequest in bookingRequests)
        {
            await ProcessBookingRequest(bookingRequest, unitOfWork, cancellationToken);

            processedProcesses++;
        }

        await unitOfWork.Commit(cancellationToken);
        logger.LogInformation("Rollback {ProcessesCount} processes", processedProcesses);
    }

    private async Task ProcessBookingRequest(
        BookingRequest bookingRequest,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var eventsToRollback = bookingRequest.GetEventsToRollback().ToArray();

        foreach (var eventToRollback in eventsToRollback)
        {
            await RollbackEvent(unitOfWork, bookingRequest, eventToRollback, cancellationToken);
        }
    }

    private async Task RollbackEvent(
        IUnitOfWork unitOfWork,
        BookingRequest bookingRequest,
        BookingEvent eventToRollback,
        CancellationToken cancellationToken)
    {
        var processor = processors.FirstOrDefault(t => t.PayloadType == eventToRollback.Payload.GetType());

        if (processor is not null)
        {
            try
            {
                var result = await processor.RollbackEvent(unitOfWork, eventToRollback, cancellationToken);
                await ProcessResult(bookingRequest, result, eventToRollback);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing event: [{EventId}]", eventToRollback.Id);
                await ProcessResult(
                    bookingRequest,
                    RollBackEventResultType.Failed,
                    eventToRollback);
            }
        }

        logger.LogInformation("No processor found for {Name}", eventToRollback.Payload.GetType().Name);
    }

    private static async Task ProcessResult(
        BookingRequest bookingRequest,
        RollBackEventResultType result,
        BookingEvent @event)
    {
        switch (result)
        {
            case RollBackEventResultType.Skipped:
                break;
            case RollBackEventResultType.Failed:
                bookingRequest.BookingProcess!.MarkRollbackAttemptFailed();
                break;
            case RollBackEventResultType.RolledBack:
                bookingRequest.BookingProcess!.RollBackEvent(@event.Id);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }
    }
}
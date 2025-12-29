using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestInitiatedEventProcessor(ILogger<BookingRequestInitiatedEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestInitiatedEventPayload>
{
    public Type PayloadType => typeof(BookingRequestInitiatedEventPayload);

    public async Task<ProcessorResult> ProcessEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Initiate booking request... Booking request: [{BookingRequestId}]", bookingEvent.BookingRequestId);

            var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingEvent.BookingRequestId), cancellationToken);
            bookingRequest.SendForApprovalInEdms();

            return new ProcessorResult(bookingEvent, ResultType.Success);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing event: [{EventId}]", bookingEvent.Id);
            return new ProcessorResult(bookingEvent, ResultType.Retry);
        }
    }

    public Task RollbackEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        if (bookingEvent.RolledBack)
        {
            return Task.CompletedTask;
        }

        logger.LogInformation("Rollback initiate booking request event... Booking request: [{EventId}]", bookingEvent.Id);
        bookingEvent.Rollback();

        return Task.CompletedTask;
    }
}
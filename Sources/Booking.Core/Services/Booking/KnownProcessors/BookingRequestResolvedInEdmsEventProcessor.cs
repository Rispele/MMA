using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
using Commons.Domain.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestResolvedInEdmsEventProcessor(
    ILogger<BookingRequestResolvedInEdmsEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestResolvedInEdmsEventPayload>
{
    public Type PayloadType => typeof(BookingRequestResolvedInEdmsEventPayload);

    public async Task<ProcessorResult> ProcessEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Saving edms resolution result. Booking request [{BookingRequestId}]...", bookingEvent.BookingRequestId);

            var bookingRequestId = bookingEvent.BookingRequestId;
            var payload = bookingEvent.Payload.GetPayload<BookingRequestResolvedInEdmsEventPayload>();

            var result = await SaveEdmsResolutionResult(unitOfWork, bookingRequestId, payload, cancellationToken);
            return new ProcessorResult(bookingEvent, result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing edms resolution result. Booking request [{BookingRequestId}]...", bookingEvent.BookingRequestId);
            return new ProcessorResult(bookingEvent, ResultType.Failure);
        }
    }

    public Task RollbackEvent(IUnitOfWork unitOfWork, BookingEvent bookingEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static async Task<ResultType> SaveEdmsResolutionResult(
        IUnitOfWork unitOfWork,
        int bookingRequestId,
        BookingRequestResolvedInEdmsEventPayload payload,
        CancellationToken cancellationToken)
    {
        var bookingRequest = await unitOfWork.ApplyQuery(new GetBookingRequestByIdQuery(bookingRequestId), cancellationToken);

        bookingRequest.SaveEdmsResolutionResult(payload.IsApproved);
        if (!payload.IsApproved)
        {
            return ResultType.RollbackInitiated;
        }

        bookingRequest.SendForModeration();

        return ResultType.Success;
    }
}
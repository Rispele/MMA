using Booking.Core.Queries.BookingRequest;
using Booking.Core.Services.Booking.KnownProcessors.Result;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Propagated.BookingRequests;
using Commons.Domain.Queries.Abstractions;
using Commons.ExternalClients.Booking;
using Commons.ExternalClients.Booking.Models;
using Microsoft.Extensions.Logging;
using Rooms.Core.Interfaces.Services.Rooms;

namespace Booking.Core.Services.Booking.KnownProcessors;

public class BookingRequestInitiatedEventProcessor(
    IRoomService roomService,
    IBookingClient bookingClient,
    bool skipTeacherPkey,
    ILogger<BookingRequestInitiatedEventProcessor> logger)
    : IBookingEventProcessor<BookingRequestInitiatedEventPayload>
{
    public Type PayloadType => typeof(BookingRequestInitiatedEventPayload);

    public async Task<SynchronizeEventProcessorResult> ProcessEvent(
        IUnitOfWork unitOfWork,
        BookingEvent bookingEvent,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation(
                "Initiate booking request... Booking request: [{BookingRequestId}]",
                bookingEvent.BookingRequestId);

            var bookingRequest = await unitOfWork.ApplyQuery(
                new GetBookingRequestByIdQuery(bookingEvent.BookingRequestId),
                cancellationToken);

            var bookingProcess = bookingRequest.BookingProcess!;

            var payloads = bookingProcess
                .GetEventsOfType<BookingRequestRoomBookedForDayEventPayload>()
                .Select(t => t.Payload.GetPayload<BookingRequestRoomBookedForDayEventPayload>())
                .ToArray();

            var datesToBook = FilterBookingPeriods(bookingRequest, payloads);

            var roomIdToScheduleId = await FetchRoomScheduleIds(datesToBook, cancellationToken);
            var roomIdsWithNotSpecifiedScheduleId = roomIdToScheduleId
                .Where(t => t.Value is null)
                .Select(t => t.Key)
                .ToArray();
            if (roomIdsWithNotSpecifiedScheduleId.Length != 0)
            {
                logger.LogError(
                    "Could not book rooms: [{RoomId}] because schedule id is undefined",
                    roomIdsWithNotSpecifiedScheduleId);

                return new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Rollback);
            }

            foreach (var dateToBook in datesToBook)
            {
                var result = await BookRoom(roomIdToScheduleId, dateToBook, bookingRequest, cancellationToken);
                if (!result.IsOk)
                {
                    return ProcessErrorResult(bookingEvent, dateToBook, result);
                }

                var payload = new BookingRequestRoomBookedForDayEventPayload(
                    dateToBook.Key,
                    dateToBook.Number,
                    result.Result);

                bookingProcess.AddBookingEvent(new BookingEvent(bookingRequest.Id, payload));
            }

            bookingRequest.SendForApprovalInEdms();

            return new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Success);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing event: [{EventId}]", bookingEvent.Id);
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

        logger.LogInformation(
            "Rollback initiate booking request event... Booking request: [{EventId}]",
            bookingEvent.Id);

        try
        {
            var bookingRequest = await unitOfWork.ApplyQuery(
                new GetBookingRequestByIdQuery(bookingEvent.BookingRequestId),
                cancellationToken);

            var bookingProcess = bookingRequest.BookingProcess!;
            var toRollback = bookingProcess
                .GetEventsOfType<BookingRequestRoomBookedForDayEventPayload>()
                .Where(t => !t.RolledBack);

            foreach (var @event in toRollback)
            {
                var scheduleEventId = @event.Payload.GetPayload<BookingRequestRoomBookedForDayEventPayload>().EventId;

                var response = await bookingClient.DeclineBooking(scheduleEventId, cancellationToken);

                if (response is { IsOk: false, ShouldRetry: true })
                {
                    return RollBackEventResultType.Failed;
                }

                if (response is { IsOk: false, ShouldRetry: false })
                {
                    logger.LogWarning(
                        "Error occured declining booking with eventId: [{Id}] of event: [{EventId}], errors: [{Errors}]. Set rolled back anyway.",
                        scheduleEventId,
                        bookingEvent.Id,
                        response.Errors);
                }

                bookingProcess.RollBackEvent(@event.Id);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occured while rolling back event: [{EventId}]", bookingEvent.Id);
            return RollBackEventResultType.Failed;
        }

        return RollBackEventResultType.RolledBack;
    }

    private SynchronizeEventProcessorResult ProcessErrorResult(
        BookingEvent bookingEvent,
        BookingPeriod dateToBook,
        BookingResponse<long> result)
    {
        logger.LogError(
            "Could not book room [{RoomId}] for date: {Date} from [{TimeFrom}] to [{TimeTo}]. Errors: [{Errors}]",
            dateToBook.RoomId,
            dateToBook.Date,
            dateToBook.From,
            dateToBook.To,
            result.Errors);

        if (result.ShouldRetry)
        {
            return new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Retry);
        }

        if (result.Errors?.Any(t => t.Contains("Аудитория занята или не существует")) ?? false)
        {
            logger.LogCritical(
                "Either event id is lost for [{RoomId}] room booking or room was already booked by someone else. Date: [{Date}] from [{TimeFrom}] to [{TimeTo}]. Need admin actions!",
                dateToBook.RoomId,
                dateToBook.Date,
                dateToBook.From,
                dateToBook.To);
        }

        return new SynchronizeEventProcessorResult(bookingEvent, SynchronizeEventResultType.Rollback);
    }

    private async Task<BookingResponse<long>> BookRoom(
        Dictionary<int, int?> roomIdToScheduleId,
        BookingPeriod dateToBook,
        BookingRequest bookingRequest,
        CancellationToken cancellationToken)
    {
        var scheduleId = roomIdToScheduleId[dateToBook.RoomId]!.Value;

        var request = new BookRoomRequest(
            dateToBook.Date,
            scheduleId.ToString()!,
            dateToBook.From,
            dateToBook.To,
            bookingRequest.EventName,
            "Мероприятие",
            bookingRequest.RoomEventCoordinator.EventCoordinatorType switch
            {
                RoomEventCoordinatorType.Student => "Студенческое мероприятие",
                RoomEventCoordinatorType.Scientific => "Научное мероприятие",
                RoomEventCoordinatorType.Other => "Прочее мероприятие",
                _ => throw new ArgumentOutOfRangeException()
            },
            skipTeacherPkey ? null : bookingRequest.EventHost.Id);

        var result = await bookingClient.BookRoom(request, cancellationToken);
        return result;
    }

    private BookingPeriod[] FilterBookingPeriods(
        BookingRequest bookingRequest,
        BookingRequestRoomBookedForDayEventPayload[] payloads)
    {
        return GetBookingPeriods(bookingRequest)
            .Where(t => payloads.All(p => p.BookingTimeKey != t.Key || p.Number != t.Number))
            .ToArray();
    }

    private static IEnumerable<BookingPeriod> GetBookingPeriods(BookingRequest request)
    {
        return request.BookingSchedule.SelectMany(GetBookingPeriodsForBookingTime);

        IEnumerable<BookingPeriod> GetBookingPeriodsForBookingTime(BookingTime time)
        {
            if (time.Frequency is BookingFrequency.Undefined)
            {
                yield return new BookingPeriod(time.Key, 0, time.Date, time.TimeFrom, time.TimeTo, time.RoomId);
                yield break;
            }

            var currentDate = time.Date;
            for (var number = 0; currentDate <= time.BookingFinishDate; number++, currentDate = currentDate.AddDays(1))
            {
                yield return new BookingPeriod(time.Key, number, currentDate, time.TimeFrom, time.TimeTo, time.RoomId);
            }
        }
    }

    private async Task<Dictionary<int, int?>> FetchRoomScheduleIds(
        BookingPeriod[] datesToBook,
        CancellationToken cancellationToken)
    {
        var roomIdsToFetch = datesToBook.Select(t => t.RoomId).Distinct().ToArray();
        var rooms = await roomService.FindRoomByIds(roomIdsToFetch, cancellationToken);
        return rooms.ToDictionary(t => t.Id, t => t.ScheduleAddress?.ScheduleRoomId);
    }

    private record BookingPeriod(Guid Key, int Number, DateOnly Date, TimeOnly From, TimeOnly To, int RoomId);
}
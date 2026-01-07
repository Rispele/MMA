using Booking.Domain.Exceptions;
using Booking.Domain.Models.BookingProcesses;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Booking.Domain.Propagated.BookingRequests;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingRequests;

[GenerateFieldNames]
public class BookingRequest
{
    private readonly int? id;

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private BookingRequest()
    {
    }

    public BookingRequest(
        BookingCreator creator,
        string reason,
        int participantsCount,
        bool techEmployeeRequired,
        string eventHostFullName,
        IRoomEventCoordinator roomEventCoordinator,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule)
    {
        Creator = creator;
        Reason = reason;
        ParticipantsCount = participantsCount;
        TechEmployeeRequired = techEmployeeRequired;
        EventHostFullName = eventHostFullName;
        RoomEventCoordinator = roomEventCoordinator;
        EventName = eventName;
        BookingSchedule = bookingSchedule;
        Status = BookingStatus.New;
        BookingScheduleStatus = Propagated.BookingRequests.BookingScheduleStatus.NotSent;
        ModeratorComment = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
    public BookingCreator Creator { get; private set; } = null!;
    public string Reason { get; private set; } = null!;
    public int ParticipantsCount { get; private set; }
    public bool TechEmployeeRequired { get; private set; }
    public string EventHostFullName { get; private set; } = null!;
    public IRoomEventCoordinator RoomEventCoordinator { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public string EventName { get; private set; } = null!;
    public BookingStatus Status { get; private set; }
    public string? ModeratorComment { get; private set; }
    public BookingScheduleStatus? BookingScheduleStatus { get; private set; }

    public IEnumerable<BookingTime> BookingSchedule { get; set; } = [];
    public BookingProcess? BookingProcess { get; private set; }

    #region Update Actions

    public void Update(
        BookingCreator creator,
        string reason,
        int participantsCount,
        bool techEmployeeRequired,
        string eventHostFullName,
        IRoomEventCoordinator roomEventCoordinator,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule)
    {
        ValidateStatus(BookingStatus.New, errorMessage: "Текущее состояние заявки не позволяет изменить её.");

        Creator = creator;
        Reason = reason;
        ParticipantsCount = participantsCount;
        TechEmployeeRequired = techEmployeeRequired;
        EventHostFullName = eventHostFullName;
        RoomEventCoordinator = roomEventCoordinator;
        EventName = eventName;
        BookingSchedule = bookingSchedule;
    }

    #endregion

    #region Edms

    public void SendForApprovalInEdms()
    {
        ValidateStatus(BookingStatus.Initiated,
            errorMessage: "Текущее состояние заявки не позволяет отправить её на согласование в СЭД.");

        Status = BookingStatus.SentForApprovalInEdms;
        BookingProcess!.AddBookingEvent(new BookingEvent(Id, new BookingRequestSentForApprovalInEdmsEventPayload()));
    }

    public void SaveEdmsResolutionResult(bool isApproved)
    {
        ValidateStatus(
            BookingStatus.SentForApprovalInEdms,
            isApproved
                ? "Текущее состояние заявки не позволяет согласовать в СЭД."
                : "Текущее состояние заявки не позволяет отказать в согласовании в СЭД.");

        Status = isApproved ? BookingStatus.ApprovedInEdms : BookingStatus.RejectedInEdms;
    }

    #endregion

    #region Moderation

    public void SendForModeration()
    {
        ValidateStatus(BookingStatus.ApprovedInEdms,
            errorMessage: "Текущее состояние заявки не позволяет отправить её на модерацию.");

        Status = BookingStatus.SentForModeration;
        // return new BookingEvent(Id, new BookingRequestSentForModerationEventPayload());
    }

    public void SaveModerationResult(bool isApproved, string moderatorComment)
    {
        ValidateStatus(
            BookingStatus.SentForModeration,
            isApproved
                ? "Текущее состояние заявки не позволяет подтвердить модератором."
                : "Текущее состояние заявки не позволяет отказать в согласовании модератором.");

        Status = isApproved ? BookingStatus.ApprovedByModerator : BookingStatus.RejectedByModerator;
        ModeratorComment = moderatorComment;
    }

    #endregion

    #region Booking Process

    public void InitiateBookingProcess()
    {
        ValidateStatus(BookingStatus.New,
            errorMessage: "Текущее состояние заявки не позволяет инициировать процесс бронирования");

        Status = BookingStatus.Initiated;

        BookingProcess = new BookingProcess(Id);
        BookingProcess.AddBookingEvent(new BookingEvent(Id, new BookingRequestInitiatedEventPayload()));
    }

    public void MarkEventProcessAttemptSucceeded(int eventId)
    {
        ValidateBookingProcessInitiated();

        BookingProcess!.MarkEventProcessAttemptSucceeded(eventId);
    }

    public void MarkEventProcessAttemptFailed(int eventId)
    {
        ValidateBookingProcessInitiated();

        BookingProcess!.MarkEventProcessAttemptFailed(eventId);
    }

    public void InitiateBookingProcessRollback()
    {
        ValidateBookingProcessInitiated();

        BookingProcess!.InitiateRollback();
    }

    public IEnumerable<BookingEvent> GetEventsToRollback()
    {
        ValidateBookingProcessInitiated();

        return BookingProcess!.GetEventsToRollback();
    }

    private void ValidateBookingProcessInitiated()
    {
        if (BookingProcess is null)
        {
            throw new InvalidOperationException("Booking process is not initiated");
        }
    }

    #endregion


    private void ValidateStatus(BookingStatus expectedStatus, string errorMessage)
    {
        if (Status == expectedStatus)
        {
            return;
        }

        throw new InvalidBookingRequestState(EnhanceMessageWithStatus(errorMessage));

        string EnhanceMessageWithStatus(string message)
        {
            return message.Trim() + $" Текущий статус заявки: [{Status}]";
        }
    }

    # region ForTests

    public BookingRequest(
        int id,
        BookingCreator creator,
        string reason,
        int participantsCount,
        bool techEmployeeRequired,
        string eventHostFullName,
        IRoomEventCoordinator roomEventCoordinator,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule) :
        this(
            creator,
            reason,
            participantsCount,
            techEmployeeRequired,
            eventHostFullName,
            roomEventCoordinator,
            eventName,
            bookingSchedule)
    {
        this.id = id;
    }

    # endregion
}
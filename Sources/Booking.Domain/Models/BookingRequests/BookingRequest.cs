using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
using Booking.Domain.Exceptions;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Booking.Domain.Propagated.BookingRequests;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingRequests;

[GenerateFieldNames]
public class BookingRequest
{
    private readonly int? id;
    private List<int> roomIds = null!;

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
        DateTime createdAt,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule,
        BookingStatus status,
        string? moderatorComment,
        BookingScheduleStatus? bookingScheduleStatus,
        List<int> roomIds)
    {
        Creator = creator;
        Reason = reason;
        ParticipantsCount = participantsCount;
        TechEmployeeRequired = techEmployeeRequired;
        EventHostFullName = eventHostFullName;
        CreatedAt = createdAt;
        RoomEventCoordinator = roomEventCoordinator;
        EventName = eventName;
        BookingSchedule = bookingSchedule;
        Status = status;
        ModeratorComment = moderatorComment;
        BookingScheduleStatus = bookingScheduleStatus;
        this.roomIds = roomIds;
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

    public IEnumerable<int> RoomIds => roomIds;
    public IEnumerable<BookingTime> BookingSchedule { get; set; } = [];

    public void Update(
        BookingCreator creator,
        string reason,
        int participantsCount,
        bool techEmployeeRequired,
        string eventHostFullName,
        IRoomEventCoordinator roomEventCoordinator,
        DateTime createdAt,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule,
        BookingStatus status,
        string? moderatorComment,
        BookingScheduleStatus? bookingScheduleStatus)
    {
        ValidateStatus(BookingStatus.New, errorMessage: "Текущее состояние заявки не позволяет изменить её.");

        Creator = creator;
        Reason = reason;
        ParticipantsCount = participantsCount;
        TechEmployeeRequired = techEmployeeRequired;
        EventHostFullName = eventHostFullName;
        RoomEventCoordinator = roomEventCoordinator;
        CreatedAt = createdAt;
        EventName = eventName;
        BookingSchedule = bookingSchedule;
        Status = status;
        ModeratorComment = moderatorComment;
        BookingScheduleStatus = bookingScheduleStatus;
    }

    public void SetRooms(List<int> roomsToSet)
    {
        ValidateStatus(BookingStatus.New, errorMessage: "Текущее состояние заявки не позволяет изменить её.");

        roomIds.Clear();
        roomIds = roomsToSet;
    }

    public BookingEvent SendForApprovalInEdms()
    {
        ValidateStatus(BookingStatus.New, errorMessage: "Текущее состояние заявки не позволяет отправить её на согласование в СЭД.");

        return new BookingEvent(Id, new BookingRequestSentForApprovalInEdmsEventPayload());
    }

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
        DateTime createdAt,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule,
        BookingStatus status,
        string? moderatorComment,
        BookingScheduleStatus? bookingScheduleStatus,
        List<int> roomIds) :
        this(
            creator,
            reason,
            participantsCount,
            techEmployeeRequired,
            eventHostFullName,
            roomEventCoordinator,
            createdAt,
            eventName,
            bookingSchedule,
            status,
            moderatorComment,
            bookingScheduleStatus,
            roomIds)
    {
        this.id = id;
    }

    # endregion
}
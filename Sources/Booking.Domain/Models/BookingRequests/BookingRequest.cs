using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Booking.Domain.Models.BookingRequests;

[GenerateFieldNames]
public class BookingRequest
{
    private readonly int? id = null;
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
        roomIds.Clear();
        roomIds = roomsToSet;
    }
}
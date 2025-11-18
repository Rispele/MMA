using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;

namespace Rooms.Domain.Models.BookingRequests;

[GenerateFieldNames]
public class BookingRequest
{
    private int? id = null;
    private readonly List<Room.Room> rooms = [];

    [UsedImplicitly(Reason = "For EF Core reasons")]
    private BookingRequest()
    {
    }

    public int Id => id ?? throw new InvalidOperationException();
    public BookingCreator Creator { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public int ParticipantsCount { get; set; }
    public bool TechEmployeeRequired { get; set; }
    public string EventHostFullName { get; set; } = null!;
    public BookingEventType EventType { get; set; }
    public string? CoordinatorInstitute { get; set; }
    public string? CoordinatorFullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string EventName { get; set; } = null!;
    public IReadOnlyList<Room.Room> Rooms => rooms;
    public IEnumerable<BookingTime> BookingSchedule { get; set; } = [];
    public BookingStatus Status { get; set; }
    public string? ModeratorComment { get; set; } = null!;
    public BookingScheduleStatus? BookingScheduleStatus { get; set; }

    public BookingRequest(
        BookingCreator creator,
        string reason,
        int participantsCount,
        bool techEmployeeRequired,
        string eventHostFullName,
        BookingEventType eventType,
        string? coordinatorInstitute,
        string? coordinatorFullName,
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
        EventType = eventType;
        CoordinatorInstitute = coordinatorInstitute;
        CoordinatorFullName = coordinatorFullName;
        CreatedAt = createdAt;
        EventName = eventName;
        BookingSchedule = bookingSchedule;
        Status = status;
        ModeratorComment = moderatorComment;
        BookingScheduleStatus = bookingScheduleStatus;
    }

    public void Update(
        BookingCreator creator,
        string reason,
        int participantsCount,
        bool techEmployeeRequired,
        string eventHostFullName,
        BookingEventType eventType,
        string? coordinatorInstitute,
        string? coordinatorFullName,
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
        EventType = eventType;
        CoordinatorInstitute = coordinatorInstitute;
        CoordinatorFullName = coordinatorFullName;
        CreatedAt = createdAt;
        EventName = eventName;
        BookingSchedule = bookingSchedule;
        Status = status;
        ModeratorComment = moderatorComment;
        BookingScheduleStatus = bookingScheduleStatus;
    }
}
using JetBrains.Annotations;
using PrivateFieldNamesExposingGenerator.Attributes;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Domain.Models.BookingRequests;

[GenerateFieldNames]
public class BookingRequest
{
    private readonly int? id = null;
    private readonly List<Room> rooms = null!;

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
        BookingEventType eventType,
        string? coordinatorInstitute,
        string? coordinatorFullName,
        DateTime createdAt,
        string eventName,
        IEnumerable<BookingTime> bookingSchedule,
        BookingStatus status,
        string? moderatorComment,
        BookingScheduleStatus? bookingScheduleStatus,
        List<Room> rooms)
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
        this.rooms = rooms;
    }

    public int Id => id ?? throw new InvalidOperationException("Id is not initialized yet");
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
    public IEnumerable<Room> Rooms => rooms;
    public IEnumerable<BookingTime> BookingSchedule { get; set; } = [];
    public BookingStatus Status { get; set; }
    public string? ModeratorComment { get; set; } = null!;
    public BookingScheduleStatus? BookingScheduleStatus { get; set; }

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
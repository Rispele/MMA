using Rooms.Domain.Models.BookingRequests;

namespace WebApi.Models.BookingRequest;

public class BookingRequestModel
{
    public int Id { get; set; }
    public BookingCreatorModel Creator { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public int ParticipantsCount { get; set; }
    public bool TechEmployeeRequired { get; set; }
    public string EventHostFullName { get; set; } = null!;
    public BookingEventType EventType { get; set; }
    public string? CoordinatorInstitute { get; set; }
    public string? CoordinatorFullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string EventName { get; set; } = null!;
    public IEnumerable<int> RoomIds { get; set; } = null!;
    public IEnumerable<BookingTimeModel> BookingSchedule { get; set; } = [];
    public BookingStatus Status { get; set; }
    public string? ModeratorComment { get; set; } = null!;
    public BookingScheduleStatus? BookingScheduleStatus { get; set; }
}
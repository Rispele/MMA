using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.Dtos.BookingRequest.Requests;

public record CreateBookingRequestDto
{
    public BookingCreatorDto Creator { get; set; } = null!;
    public string Reason { get; set; } = null!;
    public int ParticipantsCount { get; set; }
    public bool TechEmployeeRequired { get; set; }
    public string EventHostFullName { get; set; } = null!;
    public BookingEventType EventType { get; set; }
    public string? CoordinatorInstitute { get; set; }
    public string? CoordinatorFullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string EventName { get; set; } = null!;
    public IEnumerable<int> RoomIds { get; set; } = [];
    public BookingTimeDto[] BookingSchedule { get; set; } = [];
    public BookingStatus Status { get; set; }
    public string ModeratorComment { get; set; } = null!;
    public BookingScheduleStatus? BookingScheduleStatus { get; set; }
}
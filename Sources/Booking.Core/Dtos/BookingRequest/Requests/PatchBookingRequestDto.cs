using Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.Dtos.BookingRequest.Requests;

public record PatchBookingRequestDto
{
    public required BookingCreatorDto Creator { get; set; } = null!;
    public required string Reason { get; set; } = null!;
    public required int ParticipantsCount { get; set; }
    public required bool TechEmployeeRequired { get; set; }
    public required string EventHostFullName { get; set; } = null!;
    public required IRoomEventCoordinatorDto RoomEventCoordinator { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string EventName { get; set; } = null!;
    public required int[] RoomIds { get; set; } = [];
    public required BookingTimeDto[] BookingSchedule { get; set; } = [];
    public required BookingStatus Status { get; set; }
    public required string ModeratorComment { get; set; } = null!;
    public required BookingScheduleStatus? BookingScheduleStatus { get; set; }
}
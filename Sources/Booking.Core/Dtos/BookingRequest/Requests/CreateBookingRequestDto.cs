using Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Core.Dtos.BookingRequest.Requests;

public record CreateBookingRequestDto
{
    public required BookingCreatorDto Creator { get; init; }
    public required string Reason { get; init; }
    public required int ParticipantsCount { get; init; }
    public required bool TechEmployeeRequired { get; init; }
    public required string EventHostFullName { get; init; }
    public required IRoomEventCoordinatorDto RoomEventCoordinator { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string EventName { get; init; }
    public required IEnumerable<int> RoomIds { get; init; }
    public required BookingTimeDto[] BookingSchedule { get; init; }
    public required BookingStatus Status { get; init; }
    public required string ModeratorComment { get; init; }
    public required BookingScheduleStatus? BookingScheduleStatus { get; init; }
}
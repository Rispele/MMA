using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest;

public class  BookingRequestDto
{
    public required int Id { get; init; }
    public required BookingCreatorDto Creator { get; init; } = null!;
    public required string Reason { get; init; } = null!;
    public required int ParticipantsCount { get; init; }
    public required bool TechEmployeeRequired { get; init; }
    public required EventHostDto EventHost { get; init; }
    public required IRoomEventCoordinatorDto RoomEventCoordinator { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string EventName { get; init; } = null!;
    public required BookingTimeDto[] BookingSchedule { get; init; } = [];
    public required BookingStatus Status { get; init; }
    public required string? EdmsResolutionComment { get; init; }
    public required string? ModeratorComment { get; init; } = null!;
    public required BookingScheduleStatus? BookingScheduleStatus { get; init; }
}
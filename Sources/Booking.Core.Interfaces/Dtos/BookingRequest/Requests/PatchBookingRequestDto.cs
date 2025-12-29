using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;
using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.Requests;

public record PatchBookingRequestDto
{
    public required BookingCreatorDto Creator { get; set; } = null!;
    public required string Reason { get; set; } = null!;
    public required int ParticipantsCount { get; set; }
    public required bool TechEmployeeRequired { get; set; }
    public required string EventHostFullName { get; set; } = null!;
    public required IRoomEventCoordinatorDto RoomEventCoordinator { get; set; }
    public required string EventName { get; set; } = null!;
    public required BookingTimeDto[] BookingSchedule { get; set; } = [];
}
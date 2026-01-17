using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.Requests;

public record PatchBookingRequestDto
{
    public required BookingCreatorDto Creator { get; set; } = null!;
    public required string Reason { get; set; } = null!;
    public required int ParticipantsCount { get; set; }
    public required bool TechEmployeeRequired { get; set; }
    public required EventHostDto EventHost { get; init; }
    public required IRoomEventCoordinatorDto RoomEventCoordinator { get; set; }
    public required string EventName { get; set; } = null!;
    public required BookingTimeDto[] BookingSchedule { get; set; } = [];
}
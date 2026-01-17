using Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.Requests;

public record CreateBookingRequestDto
{
    public required BookingCreatorDto Creator { get; init; }
    public required string Reason { get; init; }
    public required int ParticipantsCount { get; init; }
    public required bool TechEmployeeRequired { get; init; }
    public required EventHostDto EventHost { get; init; }
    public required IRoomEventCoordinatorDto RoomEventCoordinator { get; init; }
    public required string EventName { get; init; }
    public required BookingTimeDto[] BookingSchedule { get; init; }
}
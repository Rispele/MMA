using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;

public record ScientificRoomEventCoordinatorDto(
    string CoordinatorDepartmentId,
    string CoordinatorId) : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Scientific;
}
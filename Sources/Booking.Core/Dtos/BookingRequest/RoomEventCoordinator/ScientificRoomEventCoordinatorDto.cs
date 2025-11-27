using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Booking.Core.Dtos.BookingRequest.RoomEventCoordinator;

public record ScientificRoomEventCoordinatorDto(
    string CoordinatorDepartmentId,
    string CoordinatorId) : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Scientific;
}
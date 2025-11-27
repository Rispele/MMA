using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

public record ScientificRoomEventCoordinator(
    string CoordinatorDepartmentId,
    string CoordinatorId) : IRoomEventCoordinator
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Scientific;
}
using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace WebApi.Models.BookingRequest.RoomEventCoordinator;

public record ScientificRoomEventCoordinatorModel(
    string CoordinatorDepartmentId,
    string CoordinatorId) : IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Scientific;
}
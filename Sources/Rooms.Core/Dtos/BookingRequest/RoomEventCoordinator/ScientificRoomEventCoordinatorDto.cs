using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;

public record ScientificRoomEventCoordinatorDto(
    string CoordinatorDepartmentId,
    string CoordinatorId) : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Scientific;
}
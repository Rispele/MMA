using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace WebApi.Models.BookingRequest.RoomEventCoordinator;

public record StudentRoomEventCoordinatorModel : IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Student;
}
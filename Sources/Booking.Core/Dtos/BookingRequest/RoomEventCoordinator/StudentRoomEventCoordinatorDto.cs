using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;

public record StudentRoomEventCoordinatorDto : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Student;
}
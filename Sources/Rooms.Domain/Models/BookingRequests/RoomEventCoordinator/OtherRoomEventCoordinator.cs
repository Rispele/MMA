namespace Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

public record OtherRoomEventCoordinator : IRoomEventCoordinator
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
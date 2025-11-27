namespace Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

public interface IRoomEventCoordinator
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
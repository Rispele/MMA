namespace Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

public record StudentRoomEventCoordinator : IRoomEventCoordinator
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Student;
}
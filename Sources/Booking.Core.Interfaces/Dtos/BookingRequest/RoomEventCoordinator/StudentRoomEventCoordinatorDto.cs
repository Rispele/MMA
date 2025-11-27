using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;

public record StudentRoomEventCoordinatorDto : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Student;
}
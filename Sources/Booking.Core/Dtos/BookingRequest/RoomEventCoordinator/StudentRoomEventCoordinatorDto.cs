using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Booking.Core.Dtos.BookingRequest.RoomEventCoordinator;

public record StudentRoomEventCoordinatorDto : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Student;
}
using Booking.Domain.Propagated.BookingRequests;

namespace WebApi.Models.BookingRequest.RoomEventCoordinator;

public record StudentRoomEventCoordinatorModel : IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Student;
}
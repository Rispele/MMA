using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace WebApi.Models.BookingRequest.RoomEventCoordinator;

public interface IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
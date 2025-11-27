using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Booking.Domain.Propagated.BookingRequests;

namespace WebApi.Models.BookingRequest.RoomEventCoordinator;

public interface IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
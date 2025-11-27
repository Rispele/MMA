using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;

public interface IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
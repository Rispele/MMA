using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

public record OtherRoomEventCoordinator : IRoomEventCoordinator
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
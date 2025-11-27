using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Core.Interfaces.Dtos.BookingRequest.RoomEventCoordinator;

public record OtherRoomEventCoordinatorDto : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Booking.Core.Dtos.BookingRequest.RoomEventCoordinator;

public record OtherRoomEventCoordinatorDto : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
using Booking.Domain.Propagated.BookingRequests;

namespace WebApi.Core.Models.BookingRequest.RoomEventCoordinator;

public record OtherRoomEventCoordinatorModel : IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
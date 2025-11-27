using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace WebApi.Models.BookingRequest.RoomEventCoordinator;

public record OtherRoomEventCoordinatorModel : IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
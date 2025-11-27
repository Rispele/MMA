using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;

public record OtherRoomEventCoordinatorDto : IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Other;
}
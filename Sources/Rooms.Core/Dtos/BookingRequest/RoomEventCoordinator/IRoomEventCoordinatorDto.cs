using Rooms.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Rooms.Core.Dtos.BookingRequest.RoomEventCoordinator;

public interface IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
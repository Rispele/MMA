using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

namespace Booking.Core.Dtos.BookingRequest.RoomEventCoordinator;

public interface IRoomEventCoordinatorDto
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
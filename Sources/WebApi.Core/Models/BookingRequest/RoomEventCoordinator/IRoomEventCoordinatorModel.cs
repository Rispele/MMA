using Booking.Domain.Propagated.BookingRequests;

namespace WebApi.Core.Models.BookingRequest.RoomEventCoordinator;

public interface IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
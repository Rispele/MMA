using Booking.Domain.Propagated.BookingRequests;

namespace WebApi.Core.Models.BookingRequest.RoomEventCoordinator;

public record ScientificRoomEventCoordinatorModel(
    string CoordinatorDepartmentId,
    string CoordinatorId) : IRoomEventCoordinatorModel
{
    public RoomEventCoordinatorType EventCoordinatorType => RoomEventCoordinatorType.Scientific;
}
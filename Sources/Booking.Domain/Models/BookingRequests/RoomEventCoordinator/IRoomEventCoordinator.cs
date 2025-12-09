using System.Text.Json.Serialization;
using Booking.Domain.Propagated.BookingRequests;

namespace Booking.Domain.Models.BookingRequests.RoomEventCoordinator;

[JsonDerivedType(typeof(OtherRoomEventCoordinator), nameof(OtherRoomEventCoordinator))]
[JsonDerivedType(typeof(ScientificRoomEventCoordinator), nameof(ScientificRoomEventCoordinator))]
[JsonDerivedType(typeof(StudentRoomEventCoordinator), nameof(StudentRoomEventCoordinator))]
public interface IRoomEventCoordinator
{
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
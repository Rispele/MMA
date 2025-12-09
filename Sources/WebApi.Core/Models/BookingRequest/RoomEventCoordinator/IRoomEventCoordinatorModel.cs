using System.Text.Json.Serialization;
using Booking.Domain.Propagated.BookingRequests;

namespace WebApi.Core.Models.BookingRequest.RoomEventCoordinator;

[JsonDerivedType(typeof(OtherRoomEventCoordinatorModel), nameof(OtherRoomEventCoordinatorModel))]
[JsonDerivedType(typeof(ScientificRoomEventCoordinatorModel), nameof(ScientificRoomEventCoordinatorModel))]
[JsonDerivedType(typeof(StudentRoomEventCoordinatorModel), nameof(StudentRoomEventCoordinatorModel))]
public interface IRoomEventCoordinatorModel
{
    [JsonPropertyName("$type")]
    public RoomEventCoordinatorType EventCoordinatorType { get; }
}
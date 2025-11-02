using System.Text.Json.Serialization;

namespace Rooms.Core.Dtos.Room.Fix;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomStatusDto
{
    Unspecified = 0,
    Ready = 1,
    PartiallyReady = 2,
    NotReady = 3
}
using System.Text.Json.Serialization;

namespace Rooms.Core.Dtos.Room.Parameters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomNetTypeDto
{
    Unspecified = 0,
    None = 1,
    Wired = 2,
    Wireless = 3,
    WiredAndWireless = 4
}
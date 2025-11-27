using System.Text.Json.Serialization;

namespace Rooms.Core.Interfaces.Dtos.Room.Parameters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomLayoutDto
{
    Unspecified = 0,
    Flat = 1,
    Amphitheater = 2
}
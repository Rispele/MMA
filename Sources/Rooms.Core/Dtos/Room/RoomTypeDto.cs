using System.Text.Json.Serialization;

namespace Rooms.Core.Dtos.Room;

[Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomTypeDto
{
    Unspecified = 0,
    Multimedia = 1,
    Computer = 2,
    Special = 3,
    Mixed = 4
}
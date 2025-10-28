using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Clients.Dtos.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomTypeDto
{
    Unspecified = 0,
    Multimedia = 1,
    Computer = 2,
    Special = 3,
    Mixed = 4
}
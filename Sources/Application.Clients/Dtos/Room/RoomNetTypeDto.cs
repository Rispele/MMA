using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Clients.Dtos.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomNetTypeDto
{
    Unspecified = 0,
    None = 1,
    Wired = 2,
    Wireless = 3,
    WiredAndWireless = 4
}
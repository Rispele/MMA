using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Clients.Dtos.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomLayoutDto
{
    Unspecified = 0,
    Flat = 1,
    Amphitheater = 2
}
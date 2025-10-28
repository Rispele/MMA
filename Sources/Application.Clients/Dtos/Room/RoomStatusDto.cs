using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Clients.Dtos.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomStatusDto
{
    Unspecified = 0,
    Ready = 1,
    PartiallyReady = 2,
    NotReady = 3
}

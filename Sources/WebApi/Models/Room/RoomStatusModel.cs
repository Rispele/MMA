using System.Text.Json.Serialization;

namespace WebApi.Models.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomStatusModel
{
    Unspecified = 0,
    Ready = 1,
    PartiallyReady = 2,
    NotReady = 3
}
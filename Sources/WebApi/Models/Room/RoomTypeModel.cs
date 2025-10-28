using System.Text.Json.Serialization;

namespace WebApi.Models.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomTypeModel
{
    Unspecified = 0,
    Multimedia = 1,
    Computer = 2,
    Special = 3,
    Mixed = 4
}
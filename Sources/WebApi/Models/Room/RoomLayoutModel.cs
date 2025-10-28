using System.Text.Json.Serialization;

namespace WebApi.Models.Room;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomLayoutModel
{
    Unspecified = 0,
    Flat = 1,
    Amphitheater = 2
}
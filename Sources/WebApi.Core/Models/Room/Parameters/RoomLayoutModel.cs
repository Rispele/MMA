using System.Text.Json.Serialization;

namespace WebApi.Core.Models.Room.Parameters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomLayoutModel
{
    Unspecified = 0,
    Flat = 1,
    Amphitheater = 2
}
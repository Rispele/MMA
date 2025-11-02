using System.Text.Json.Serialization;

namespace WebApi.Models.Room.Parameters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomNetTypeModel
{
    Unspecified = 0,
    None = 1,
    Wired = 2,
    Wireless = 3,
    WiredAndWireless = 4
}
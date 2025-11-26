using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.Rooms.Parameters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomLayout
{
    Unspecified = 0,

    [Description("Плоская")]
    Flat = 1,

    [Description("Амфитеатр")]
    Amphitheater = 2
}
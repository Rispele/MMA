using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Propagated.Rooms;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomLayout
{
    [Description("Не выбрано")]
    Unspecified = 0,

    [Description("Плоская")]
    Flat = 1,

    [Description("Амфитеатр")]
    Amphitheater = 2
}
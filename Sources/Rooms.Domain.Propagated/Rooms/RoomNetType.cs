using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Propagated.Rooms;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomNetType
{
    Unspecified = 0,

    [Description("Отсутствует")]
    None = 1,

    [Description("Проводная")]
    Wired = 2,

    [Description("Беспроводная")]
    Wireless = 3,

    [Description("Проводная и беспроводная")]
    WiredAndWireless = 4
}
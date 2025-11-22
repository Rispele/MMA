using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Core.Dtos.Room.Fix;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomStatusDto
{
    Unspecified = 0,

    [Description("Готова")]
    Ready = 1,

    [Description("Частично готова")]
    PartiallyReady = 2,

    [Description("Ничего не работает")]
    Malfunction = 3,
}
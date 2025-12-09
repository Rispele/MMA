using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WebApi.Core.Models.Room.Fix;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomStatusModel
{
    Unspecified = 0,

    [Description("Готова")]
    Ready = 1,

    [Description("Частично готова")]
    PartiallyReady = 2,

    [Description("Ничего не работает")]
    Malfunction = 3,
}
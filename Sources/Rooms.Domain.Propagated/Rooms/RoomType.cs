using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Propagated.Rooms;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomType
{
    Unspecified = 0,

    [Description("Мультимедийная")]
    Multimedia = 1,

    [Description("Компьютерный класс")]
    Computer = 2,

    [Description("Специализированная")]
    Special = 3,

    [Description("Смешанная")]
    Mixed = 4,
}
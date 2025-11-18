using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.Room.Parameters;

/// <summary>
/// Тип сети аудитории
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomNetType
{
    Unspecified = 0,

    /// <summary>
    /// Отсутствует
    /// </summary>
    [Description("Отсутствует")]
    None = 1,

    /// <summary>
    /// Проводная
    /// </summary>
    [Description("Проводная")]
    Wired = 2,

    /// <summary>
    /// Беспроводная
    /// </summary>
    [Description("Беспроводная")]
    Wireless = 3,

    /// <summary>
    /// Проводная и беспроводная
    /// </summary>
    [Description("Проводная и беспроводная")]
    WiredAndWireless = 4
}
using System.ComponentModel;

namespace Rooms.Domain.Models.Room.Parameters;

/// <summary>
/// Тип сети аудитории
/// </summary>
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
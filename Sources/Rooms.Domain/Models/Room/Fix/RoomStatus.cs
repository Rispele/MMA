using System.ComponentModel;

namespace Rooms.Domain.Models.Room.Fix;

/// <summary>
/// Степень готовности аудитории
/// </summary>
public enum RoomStatus
{
    Unspecified = 0,

    /// <summary>
    /// Готова
    /// </summary>
    [Description("Готова")]
    Ready = 1,

    /// <summary>
    /// Частично готова
    /// </summary>
    [Description("Частично готова")]
    PartiallyReady = 2,

    /// <summary>
    /// Ничего не работает
    /// </summary>
    [Description("Ничего не работает")]
    Malfunction = 3,
}
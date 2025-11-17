using System.ComponentModel;

namespace Rooms.Domain.Models.Room.Parameters;

/// <summary>
/// Планировка аудитории
/// </summary>
public enum RoomLayout
{
    Unspecified = 0,

    /// <summary>
    /// Плоская
    /// </summary>
    [Description("Плоская")]
    Flat = 1,

    /// <summary>
    /// Амфитеатр
    /// </summary>
    [Description("Амфитеатр")]
    Amphitheater = 2
}
using System.ComponentModel;

namespace Rooms.Domain.Models.Room.Parameters;

/// <summary>
/// Тип аудитории
/// </summary>
public enum RoomType
{
    Unspecified = 0,

    /// <summary>
    /// Мультимедийная
    /// </summary>
    [Description("Мультимедийная")]
    Multimedia = 1,

    /// <summary>
    /// Компьютерный класс
    /// </summary>
    [Description("Компьютерный класс")]
    Computer = 2,

    /// <summary>
    /// Специализированная
    /// </summary>
    [Description("Специализированная")]
    Special = 3,

    /// <summary>
    /// Смешанная
    /// </summary>
    [Description("Смешанная")]
    Mixed = 4,
}
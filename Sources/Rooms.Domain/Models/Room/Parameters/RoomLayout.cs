using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.Room.Parameters;

/// <summary>
/// Планировка аудитории
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
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
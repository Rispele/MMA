using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.Equipments;

/// <summary>
/// Статус оборудования
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EquipmentStatus
{
    /// <summary>
    /// Исправно
    /// </summary>
    [Description("Исправно")]
    Ok = 1,

    /// <summary>
    /// Неисправно
    /// </summary>
    [Description("Неисправно")]
    Malfunction = 2,

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    [Description("Сообщение об ошибке")]
    Error = 3
}
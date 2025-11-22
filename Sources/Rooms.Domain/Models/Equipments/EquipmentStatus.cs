using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Rooms.Domain.Models.Equipments;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EquipmentStatus
{
    [Description("Исправно")]
    Ok = 1,

    [Description("Неисправно")]
    Malfunction = 2,

    [Description("Сообщение об ошибке")]
    Error = 3
}
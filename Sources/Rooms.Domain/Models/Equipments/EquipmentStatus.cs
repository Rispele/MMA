namespace Rooms.Domain.Models.Equipments;

public enum EquipmentStatus
{
    /// <summary>
    /// Исправно
    /// </summary>
    Ok = 1,

    /// <summary>
    /// Неисправно
    /// </summary>
    Malfunction = 2,

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    Error = 3
}
namespace Rooms.Domain.Models.Equipments;

/// <summary>
/// Параметр оборудования
/// </summary>
public class EquipmentParameterDescriptor
{
    /// <summary>
    /// Название параметра
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Обязательность параметра
    /// </summary>
    public bool Required { get; set; }
}
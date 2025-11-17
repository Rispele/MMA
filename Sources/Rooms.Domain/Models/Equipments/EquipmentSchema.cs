namespace Rooms.Domain.Models.Equipments;

/// <summary>
/// Модель оборудования
/// </summary>
public class EquipmentSchema
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название модели оборудования
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Тип оборудования
    /// </summary>
    public required EquipmentType Type { get; set; } = null!;

    /// <summary>
    /// Значения параметров типа оборудования
    /// </summary>
    public required Dictionary<string, string> ParameterValues { get; set; }

    public void Update(
        string name,
        EquipmentType equipmentType,
        Dictionary<string, string> parameterValues)
    {
        Name = name;
        Type = equipmentType;
        ParameterValues = parameterValues;
    }
}
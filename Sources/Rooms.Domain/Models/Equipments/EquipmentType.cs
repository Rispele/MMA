namespace Rooms.Domain.Models.Equipments;

/// <summary>
/// Тип оборудования
/// </summary>
public class EquipmentType
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название типа оборудования
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Параметры типа оборудования
    /// </summary>
    public List<EquipmentParameterDescriptor> Parameters { get; set; } = [];

    public void Update(
        string name,
        List<EquipmentParameterDescriptor> parameters)
    {
        Name = name;
        Parameters = parameters;
    }
}
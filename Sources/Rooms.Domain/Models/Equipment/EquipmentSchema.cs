namespace Rooms.Domain.Models.Equipment;

public class EquipmentSchema
{
    public int Id { get; set; }
    public EquipmentType Type { get; set; } = default!;
    public Dictionary<string, string> ParameterValues { get; set; } = default!; // key = EquipmentParameterDescriptor.Name, value = InputValue as string - валидируем соответствие дескриптору
}
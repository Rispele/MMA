namespace Rooms.Core.Dtos.Equipment;

public class EquipmentSchemaDto
{
    public int Id { get; set; }
    public EquipmentTypeDto TypeDto { get; set; } = default!;
    public Dictionary<string, string> ParameterValues { get; set; } = default!; // key = EquipmentParameterDescriptorDto.Name, value = InputValue as string - валидируем соответствие дескриптору
}
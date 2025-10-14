namespace WebApi.Models.Equipment;

public class EquipmentSchemaModel
{
    public int Id { get; init; }
    public EquipmentTypeModel TypeModel { get; init; } = default!;
    public Dictionary<string, string> ParameterValues { get; init; } = default!; // key = EquipmentParameterDescriptorDto.Name, value = InputValue as string - валидируем соответствие дескриптору
}
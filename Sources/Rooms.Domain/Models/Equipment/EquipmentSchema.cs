using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Persistence;

namespace Rooms.Domain.Models.Equipment;

[EntityTypeConfiguration<EquipmentSchemaEntityTypeConfiguration, EquipmentSchema>]
public class EquipmentSchema
{
    public int Id { get; set; }
    public EquipmentType Type { get; set; } = default!;
    public Dictionary<string, string> ParameterValues { get; set; } = default!; // key = EquipmentParameterDescriptor.Name, value = InputValue as string - валидируем соответствие дескриптору
    public ICollection<Equipment> Equipments { get; set; }
}
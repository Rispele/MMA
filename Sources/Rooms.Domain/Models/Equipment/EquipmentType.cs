using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Persistence;

namespace Rooms.Domain.Models.Equipment;

[EntityTypeConfiguration<EquipmentTypeEntityTypeConfiguration, EquipmentType>]
public class EquipmentType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public EquipmentParameterDescriptor[] Parameters { get; set; }
    public ICollection<EquipmentSchema> Schemas { get; set; }
}
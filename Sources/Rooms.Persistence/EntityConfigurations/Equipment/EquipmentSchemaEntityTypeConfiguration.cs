using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Persistence.EntityConfigurations.Equipment;

public class EquipmentSchemaEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentSchema>
{
    public void Configure(EntityTypeBuilder<EquipmentSchema> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.HasOne(x => x.EquipmentType).WithMany(x => x.Schemas).HasForeignKey(x => x.EquipmentTypeId);
        builder.Property(x => x.ParameterValues).HasColumnType("jsonb");
        builder.HasMany(x => x.Equipments).WithOne(x => x.Schema).HasForeignKey(x => x.SchemaId);
    }
}
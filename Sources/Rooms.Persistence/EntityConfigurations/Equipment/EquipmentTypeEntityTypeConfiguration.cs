using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Persistence.EntityConfigurations.Equipment;

public class EquipmentTypeEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
{
    public void Configure(EntityTypeBuilder<EquipmentType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Parameters).HasColumnType("jsonb");
        builder.HasMany(x => x.Schemas).WithOne(x => x.EquipmentType).HasForeignKey(x => x.EquipmentTypeId);
    }
}
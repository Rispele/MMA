using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.Equipments;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.Equipment;

public class EquipmentTypeEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
{
    public void Configure(EntityTypeBuilder<EquipmentType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasField(EquipmentTypeFieldNames.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Parameters).HasField(EquipmentTypeFieldNames.Parameters).HasColumnType("jsonb");
    }
}
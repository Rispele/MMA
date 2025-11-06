using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rooms.Persistence.EntityConfigurations.Equipment;

public class EquipmentEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.Equipments.Equipment>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Equipments.Equipment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Schema).WithMany();

        builder.Property(t => t.InventoryNumber).HasMaxLength(256);
        builder.Property(t => t.SerialNumber).HasMaxLength(256);
        builder.Property(t => t.NetworkEquipmentIp).HasMaxLength(256);
        builder.Property(t => t.Comment).HasMaxLength(256);
    }
}
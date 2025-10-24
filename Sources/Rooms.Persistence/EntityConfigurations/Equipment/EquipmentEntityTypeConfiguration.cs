using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Rooms.Persistence.EntityConfigurations.Equipment;

public class EquipmentEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Models.Equipment.Equipment>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Equipment.Equipment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Room).IsRequired();
        builder.Property(x => x.Schema).IsRequired();

        builder.HasOne(x => x.Room).WithMany(x => x.Equipments).HasForeignKey(x => x.Id);

        builder.HasOne(x => x.Schema).WithMany(x => x.Equipments).HasForeignKey(x => x.Id);

        builder.Property(t => t.InventoryNumber).HasMaxLength(256);
        builder.Property(t => t.SerialNumber).HasMaxLength(256);
        builder.Property(t => t.NetworkEquipmentIp).HasMaxLength(256);
        builder.Property(t => t.Comment).HasMaxLength(256);
    }
}
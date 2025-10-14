using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.Equipment;

namespace Rooms.Domain.Persistence;

public class EquipmentEntityTypeConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Schema).IsRequired();

        builder.OwnsOne(
            t => t.Room,
            b =>
            {
            });

        builder.OwnsOne(
            t => t.Type,
            _ =>
            {
            });

        builder.OwnsOne(
            t => t.Schema,
            _ =>
            {
            });

        builder.Property(t => t.Comment).HasMaxLength(256);
    }
}
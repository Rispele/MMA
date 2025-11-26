using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.Rooms;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.Rooms;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasField(RoomFieldNames.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(256);

        builder.OwnsOne(
            navigationExpression: t => t.ScheduleAddress,
            buildAction: b =>
            {
                b.Property(x => x.Address).HasMaxLength(64);
                b.Property(x => x.RoomNumber).HasMaxLength(32);
            });

        builder.OwnsOne(
            navigationExpression: t => t.Parameters,
            buildAction: b =>
            {
                b.Property(t => t.Type).IsRequired();
                b.Property(t => t.Layout).IsRequired();
                b.Property(t => t.NetType).IsRequired();
                b.Property(t => t.Seats);
                b.Property(t => t.ComputerSeats);
                b.Property(t => t.HasConditioning);

                b.HasIndex(t => t.Seats);
                b.HasIndex(t => t.ComputerSeats);
            });

        builder.Property(t => t.Attachments).HasColumnType("jsonb");
        builder.Property(t => t.Owner).HasMaxLength(64);
        builder.OwnsOne(
            navigationExpression: t => t.FixInfo,
            buildAction: b =>
            {
                b.Property(t => t.Status).IsRequired();
                b.Property(t => t.Comment).HasMaxLength(256);
                b.Property(t => t.FixDeadline).HasColumnType("timestamptz");
            });
        builder.Property(t => t.AllowBooking).IsRequired();

        builder
            .HasMany(t => t.Equipments)
            .WithOne()
            .HasForeignKey(t => t.RoomId);
        builder.Navigation(t => t.Equipments).HasField(RoomFieldNames.Equipments);

        builder.HasIndex(t => t.Name).IsUnique();
    }
}
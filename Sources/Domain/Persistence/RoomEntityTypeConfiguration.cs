using Domain.Models.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence;

public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
        
        builder.OwnsOne(
            t => t.ScheduleAddress,
            b =>
            {
                b.Property(x => x.Address).HasMaxLength(64);
                b.Property(x => x.RoomNumber).HasMaxLength(32);
            });
        
        builder.OwnsOne(
            t => t.Parameters,
            b =>
            {
                b.Property(t => t.Type);
                b.Property(t => t.Layout);
                b.Property(t => t.NetType);
                b.Property(t => t.Seats);
                b.Property(t => t.ComputerSeats);
                b.Property(t => t.HasConditioning);
            });
        //
        // builder.OwnsOne(
        //     t => t.Attachments,
        //     b =>
        //     {
        //         b.Property(t => t.PdfRoomScheme).
        //     })
    }
}
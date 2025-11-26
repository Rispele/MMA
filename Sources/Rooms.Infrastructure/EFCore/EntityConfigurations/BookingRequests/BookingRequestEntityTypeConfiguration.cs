using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooms.Domain.Models.BookingRequests;

namespace Rooms.Infrastructure.EFCore.EntityConfigurations.BookingRequests;

public class BookingRequestEntityTypeConfiguration : IEntityTypeConfiguration<BookingRequest>
{
    public void Configure(EntityTypeBuilder<BookingRequest> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(
            navigationExpression: x => x.Creator,
            buildAction: x =>
            {
                x.Property(c => c.FullName).IsRequired().HasMaxLength(500);
                x.Property(c => c.Email).IsRequired().HasMaxLength(500);
                x.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(500);
            });

        builder.Property(x => x.Reason).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ParticipantsCount).IsRequired();
        builder.Property(x => x.TechEmployeeRequired).IsRequired();
        builder.Property(x => x.EventHostFullName).IsRequired().HasMaxLength(500);
        builder.Property(x => x.EventType).IsRequired();
        builder.Property(x => x.CoordinatorInstitute);
        builder.Property(x => x.CoordinatorFullName);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
        builder.Property(x => x.EventName).IsRequired().HasMaxLength(500);

        builder.Property(x => x.BookingSchedule).IsRequired().HasColumnType("jsonb");
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.ModeratorComment).HasMaxLength(500);
        builder.Property(x => x.BookingScheduleStatus);

        builder.HasMany(t => t.Rooms).WithMany();
        builder.Navigation(t => t.Rooms).HasField(BookingRequestFieldNames.Rooms);
    }
}
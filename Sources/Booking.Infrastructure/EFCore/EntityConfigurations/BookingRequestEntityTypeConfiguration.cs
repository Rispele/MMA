using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Booking.Domain.Models.BookingProcesses;
using Booking.Domain.Models.BookingRequests;
using Booking.Domain.Models.BookingRequests.RoomEventCoordinator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class BookingRequestEntityTypeConfiguration : IEntityTypeConfiguration<BookingRequest>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

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
        builder
            .Property(x => x.RoomEventCoordinator)
            .HasColumnType("jsonb")
            .HasConversion(
                coordinator => JsonSerializer.Serialize(coordinator, JsonSerializerOptions),
                json => JsonSerializer.Deserialize<IRoomEventCoordinator>(json, JsonSerializerOptions)!);
        builder
            .Property(x => x.EventHost)
            .HasColumnType("jsonb")
            .HasConversion(
                coordinator => JsonSerializer.Serialize(coordinator, JsonSerializerOptions),
                json => JsonSerializer.Deserialize<EventHost>(json, JsonSerializerOptions)!);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
        builder.Property(x => x.EventName).IsRequired().HasMaxLength(500);
        builder.Property(x => x.RoomIds).HasField(BookingRequestFieldNames.RoomIds).HasColumnType("jsonb");

        builder.Property(x => x.BookingSchedule).IsRequired().HasColumnType("jsonb");
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.EdmsResolutionComment).HasMaxLength(500);
        builder.Property(x => x.ModeratorComment).HasMaxLength(500);
        builder.Property(x => x.BookingScheduleStatus);

        builder.Navigation(t => t.BookingProcess).AutoInclude();
        builder
            .HasOne(t => t.BookingProcess)
            .WithOne()
            .HasForeignKey<BookingProcess>(x => x.BookingRequestId);
    }
}
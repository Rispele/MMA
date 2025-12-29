using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Booking.Domain.Models.BookingProcesses.Events;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Domain.Models.BookingRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class BookingEventEntityTypeConfiguration : IEntityTypeConfiguration<BookingEvent>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    public void Configure(EntityTypeBuilder<BookingEvent> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder
            .Property(t => t.Payload)
            .HasColumnType("jsonb")
            .HasConversion(
                coordinator => JsonSerializer.Serialize(coordinator, JsonSerializerOptions),
                json => JsonSerializer.Deserialize<IBookingEventPayload>(json, JsonSerializerOptions)!);

        builder.HasOne<BookingRequest>().WithMany().HasForeignKey(t => t.BookingRequestId);
    }
}
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Booking.Domain.Events;
using Booking.Domain.Events.Payloads;
using Booking.Domain.Models.BookingRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class BookingRequestEventEntityTypeConfiguration : IEntityTypeConfiguration<BookingRequestEvent>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    public void Configure(EntityTypeBuilder<BookingRequestEvent> builder)
    {
        builder.HasKey(t => t.EventId);
        builder.Property(t => t.EventId).HasField(BookingRequestEventFieldNames.EventId);

        builder
            .Property(t => t.Payload)
            .HasColumnType("jsonb")
            .HasConversion(
                coordinator => JsonSerializer.Serialize(coordinator, JsonSerializerOptions),
                json => JsonSerializer.Deserialize<IBookingRequestEventPayload>(json, JsonSerializerOptions)!);

        builder.HasOne<BookingRequest>().WithMany().HasForeignKey(t => t.BookingRequestId);
    }
}
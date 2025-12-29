using Booking.Domain.Models.BookingProcesses.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class BookingEventRetryContextEntityTypeConfiguration : IEntityTypeConfiguration<BookingEventRetryContext>
{
    public void Configure(EntityTypeBuilder<BookingEventRetryContext> builder)
    {
        builder.HasKey(t => t.Id);
    }
}
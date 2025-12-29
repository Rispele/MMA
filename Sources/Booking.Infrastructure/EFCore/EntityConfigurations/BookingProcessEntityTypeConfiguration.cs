using Booking.Domain.Models.BookingProcesses;
using Booking.Domain.Models.BookingProcesses.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class BookingProcessEntityTypeConfiguration : IEntityTypeConfiguration<BookingProcess>
{
    public void Configure(EntityTypeBuilder<BookingProcess> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(BookingProcessFieldNames.BookingEvents).WithOne();
        builder.HasMany(BookingProcessFieldNames.RetryContexts).WithOne();
    }
}
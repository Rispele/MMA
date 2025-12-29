using Booking.Domain.Models.BookingProcesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.EFCore.EntityConfigurations;

public class BookingProcessEntityTypeConfiguration : IEntityTypeConfiguration<BookingProcess>
{
    public void Configure(EntityTypeBuilder<BookingProcess> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Navigation(t => t.BookingEvents).HasField(BookingProcessFieldNames.BookingEvents).AutoInclude();
        builder.Navigation(t => t.BookingRetryContexts).HasField(BookingProcessFieldNames.RetryContexts).AutoInclude();

        builder.HasMany(t => t.BookingEvents).WithOne();
        builder.HasMany(t => t.BookingRetryContexts).WithOne();
    }
}
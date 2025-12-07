using Booking.Domain.Propagated.BookingRequests;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Booking.Infrastructure.Configuration;

public static class NpgsqlDbContextOptionsExtensions
{
    public static void ConfigureNpgsqlBookingDbContextOptions(this NpgsqlDbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .MapEnum<BookingFrequency>()
            .MapEnum<BookingScheduleStatus>()
            .MapEnum<BookingStatus>()
            .ConfigureDataSource(b => b.EnableDynamicJson());
    }
}
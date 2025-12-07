using Booking.Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sources.ServiceDefaults;

namespace Booking.Infrastructure.Configuration;

public static class DbContextConfigurationExtensions
{
    public static IServiceCollection ConfigureBookingDbContextForTests(this IServiceCollection services, string roomsDbContextConnectionString)
    {
        return services.ConfigurePostgresDbContext<BookingDbContext>(
            roomsDbContextConnectionString,
            NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlBookingDbContextOptions);
    }

    public static IHostApplicationBuilder AddBookingDbContext(this IHostApplicationBuilder builder)
    {
        return builder.ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, BookingDbContext>(
            KnownResourceNames.MmrDb,
            NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlBookingDbContextOptions);
    }
}
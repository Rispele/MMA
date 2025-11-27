using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Core.Interfaces.Services.InstituteCoordinators;
using Booking.Core.Services.Booking.BookingRequests;
using Booking.Core.Services.InstituteCoordinators;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure.Configuration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForBooking(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IInstituteCoordinatorsService, InstituteCoordinatorsService>()
            .AddScoped<IBookingRequestService, BookingRequestService>();
    }
}
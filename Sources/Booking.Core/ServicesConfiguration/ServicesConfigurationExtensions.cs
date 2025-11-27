using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Core.Interfaces.Services.InstituteCoordinators;
using Booking.Core.Services.Booking.BookingRequests;
using Booking.Core.Services.InstituteCoordinators;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Core.ServicesConfiguration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForBookingCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IInstituteCoordinatorsService, InstituteCoordinatorsService>()
            .AddScoped<IBookingRequestService, BookingRequestService>();
    }
}
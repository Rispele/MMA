using Booking.Core.Services.Booking.BookingRequests;
using Booking.Core.Services.Booking.BookingRequests.Interfaces;
using Booking.Core.Services.InstituteCoordinators;
using Booking.Core.Services.InstituteCoordinators.Interfaces;
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
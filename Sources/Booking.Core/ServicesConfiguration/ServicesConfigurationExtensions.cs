using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Services.Booking.BookingRequests;
using Rooms.Core.Services.Booking.BookingRequests.Interfaces;
using Rooms.Core.Services.InstituteCoordinators;
using Rooms.Core.Services.InstituteCoordinators.Interfaces;

namespace Rooms.Core.ServicesConfiguration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForRoomsCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IInstituteCoordinatorsService, InstituteCoordinatorsService>()
            .AddScoped<IBookingRequestService, BookingRequestService>();
    }
}
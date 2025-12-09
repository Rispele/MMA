using Booking.Core;
using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Core.Interfaces.Services.InstituteCoordinators;
using Booking.Core.Interfaces.Services.LkUser;
using Booking.Core.Interfaces.Services.Schedule;
using Booking.Core.Services.BookingRequests;
using Booking.Core.Services.InstituteCoordinators;
using Booking.Core.Services.LkUser;
using Booking.Core.Services.Schedule;
using Booking.Infrastructure.EFCore;
using Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;
using Commons.Domain.Queries.Factories;
using Commons.ExternalClients.RoomSchedule;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure.Configuration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForBooking(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton(new RoomScheduleClientSettings("https://public-schedule-api.my1.urfu.ru/"))
            
            .AddKeyedScoped<IUnitOfWorkFactory, BookingDbContextUnitOfWorkFactory>(KnownScopes.Booking)
            
            .AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<FilterBookingRequestsQueryHandler>(); })
            .AddScoped<IRoomScheduleClient, RoomScheduleClient>()
            .AddScoped<IInstituteCoordinatorsService, InstituteCoordinatorsService>()
            .AddScoped<IBookingRequestService, BookingRequestService>()
            .AddScoped<ILkUserService, LkUserService>()
            .AddScoped<IScheduleService, ScheduleService>();
    }
}
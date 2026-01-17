using Booking.Core;
using Booking.Core.Interfaces.Services.Booking;
using Booking.Core.Interfaces.Services.BookingRequests;
using Booking.Core.Interfaces.Services.InstituteCoordinators;
using Booking.Core.Interfaces.Services.LkUser;
using Booking.Core.Interfaces.Services.Schedule;
using Booking.Core.Services.Booking;
using Booking.Core.Services.Booking.KnownProcessors;
using Booking.Core.Services.BookingRequests;
using Booking.Core.Services.InstituteCoordinators;
using Booking.Core.Services.LkUser;
using Booking.Core.Services.Schedule;
using Booking.Domain.Models.BookingProcesses.Events.Payloads;
using Booking.Infrastructure.EFCore;
using Booking.Infrastructure.EFCore.QueryHandlers.BookingRequests;
using Commons.Domain.Queries.Factories;
using Commons.ExternalClients.Booking;
using Commons.ExternalClients.RoomSchedule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rooms.Core.Interfaces.Services.Rooms;

namespace Booking.Infrastructure.Configuration;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection ConfigureServicesForBooking(this IServiceCollection serviceCollection, bool isDevelopment)
    {
        return serviceCollection
            .AddSingleton(new RoomScheduleClientSettings("https://public-schedule-api.my1.urfu.ru/"))
            .AddKeyedScoped<IUnitOfWorkFactory, BookingDbContextUnitOfWorkFactory>(KnownScopes.Booking)
            .AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<FilterBookingRequestsQueryHandler>(); })
            .AddScoped<IBookingService, BookingService>()
            .AddScoped<IRoomScheduleClient, RoomScheduleClient>()
            .AddScoped<IInstituteCoordinatorsService, InstituteCoordinatorsService>()
            .AddScoped<IBookingRequestService, BookingRequestService>()
            .AddScoped<ILkUserService, LkUserService>()
            .AddScoped<IScheduleService, ScheduleService>()
            .AddBookingEventSynchronizer(isDevelopment);
    }

    private static IServiceCollection AddBookingEventSynchronizer(this IServiceCollection serviceCollection, bool isDevelopment)
    {
        return serviceCollection
            .AddScoped<BookingEventProcessor>()
            .AddScoped<IBookingEventRetriesSynchronizer, BookingEventRetriesSynchronizer>()
            .AddScoped<IBookingEventsSynchronizer, BookingEventsSynchronizer>()
            .AddScoped<IBookingProcessRollbackSynchronizer, BookingProcessRollbackSynchronizer>()
            .AddScoped<IBookingEventProcessor<IBookingEventPayload>, BookingRequestSentForApprovalInEdmsEventProcessor>()
            .AddScoped<IBookingEventProcessor<IBookingEventPayload>, BookingRequestResolvedInEdmsEventProcessor>()
            .AddScoped<IBookingEventProcessor<IBookingEventPayload>, BookingRequestInitiatedEventProcessor>(t =>
                new BookingRequestInitiatedEventProcessor(
                    t.GetRequiredService<IRoomService>(),
                    t.GetRequiredService<IBookingClient>(),
                    skipTeacherPkey: isDevelopment,
                    t.GetRequiredService<ILogger<BookingRequestInitiatedEventProcessor>>()));
    }
}
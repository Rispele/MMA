using Booking.Infrastructure.Configuration;
using Booking.Orchestrator;
using Commons.ExternalClients.Booking;
using Rooms.Infrastructure.Configuration;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddMinio()
    .AddScheduleApiClientSettings()
    .AddServiceDefaults()
    .AddRoomsDbContext()
    .AddBookingDbContext();

builder.Services
    .AddSingleton<IBookingClient, BookingClient>()
    .ConfigureServicesForRooms()
    .ConfigureServicesForBooking(builder.Environment.IsDevelopment());

builder.Services
    .AddHostedService<BookingOrchestrator>()
    .AddHostedService<BookingRetriesOrchestrator>()
    .AddHostedService<BookingRollbackOrchestrator>();

var host = builder.Build();
host.Run();
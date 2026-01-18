using Booking.Infrastructure.Configuration;
using Booking.Orchestrator;
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
    .ConfigureServicesForRooms()
    .ConfigureServicesForBooking(builder.Environment.IsDevelopment());

builder.Services
    .AddHostedService<BookingOrchestrator>()
    .AddHostedService<BookingRetriesOrchestrator>()
    .AddHostedService<BookingRollbackOrchestrator>();

var host = builder.Build();
host.Run();
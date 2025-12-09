using Booking.Infrastructure.Configuration;
using Booking.Orchestrator;
using Rooms.Infrastructure.Configuration;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddMinio()
    .AddServiceDefaults()
    .AddRoomsDbContext()
    .AddBookingDbContext();

builder.Services
    .ConfigureServicesForRooms()
    .ConfigureServicesForBooking();

builder.Services.AddHostedService<BookingOrchestrator>();

var host = builder.Build();
host.Run();
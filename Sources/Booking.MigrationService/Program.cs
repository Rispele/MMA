using Booking.Infrastructure.Configuration;
using Booking.MigrationService;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder
    .AddServiceDefaults()
    .AddBookingDbContext();

builder.Services
    .AddHostedService<Worker>();

var host = builder.Build();
host.Run();
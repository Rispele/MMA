using Rooms.Core.Configuration;
using Rooms.MigrationService;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder
    .AddServiceDefaults()
    .AddRoomsDbContext();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
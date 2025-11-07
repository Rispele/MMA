using Rooms.MigrationService;
using Rooms.Infrastructure;
using Rooms.Infrastructure.Configuration;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder
    .AddServiceDefaults()
    .ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
        KnownResourceNames.MmrDb,
        NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
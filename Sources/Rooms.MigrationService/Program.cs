using Rooms.Infrastructure.Configuration;
using Rooms.Infrastructure.EFCore;
using Rooms.MigrationService;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder
    .AddServiceDefaults()
    .ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
        KnownResourceNames.MmrDb,
        NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);

builder.Services
    .AddHostedService<Worker>();

var host = builder.Build();
host.Run();
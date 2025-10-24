using Rooms.MigrationService;
using Rooms.Persistence;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder
    .AddServiceDefaults()
    .ConfigurePostgresDbContextWithInstrumentation<IHostApplicationBuilder, RoomsDbContext>(
        connectionName: KnownResourceNames.MmrDb,
        NpgsqlDbContextOptionsExtensions.ConfigureNpgsqlRoomsDbContextOptions);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
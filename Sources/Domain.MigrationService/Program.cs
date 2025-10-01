using Domain.MigrationService;
using Domain.Persistence;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder
    .AddServiceDefaults()
    .AddPostgresDbContext<HostApplicationBuilder, DomainDbContext>("mmr");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
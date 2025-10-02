using Application.Configuration;
using Domain.MigrationService;
using Sources.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder
    .AddServiceDefaults()
    .AddDomainContext();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
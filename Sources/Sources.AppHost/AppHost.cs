using Sources.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUserName = builder.AddParameter("PostgresUserName", secret: true);
var postgresPassword = builder.AddParameter("PostgresUserPassword", secret: true);
var postgresPort = builder.AddParameter("PostgresPort", secret: true);

var port = await postgresPort.Resource.GetValueAsync(CancellationToken.None) ?? throw new InvalidOperationException("Port not specified");

var postgres = builder
    .AddPostgres(KnownResourceNames.PostgresDb, postgresUserName, postgresPassword, port: int.Parse(port))
    .AddDatabase("mmr");

var postgresMigrations = builder
    .AddProject<Projects.Domain_MigrationService>(KnownResourceNames.DomainMigrationService)
    .WithReference(postgres)
    .WaitFor(postgres);

var application = builder
    .AddProject<Projects.Application>(KnownResourceNames.ApplicationService)
    .WithReference(postgres)
    .WithReference(postgresMigrations)
    .WaitFor(postgresMigrations);

builder
    .AddProject<Projects.WebApi>(KnownResourceNames.WebApiService)
    .WithExternalHttpEndpoints()
    .WithReference(application)
    .WaitFor(application);

builder.Build().Run();

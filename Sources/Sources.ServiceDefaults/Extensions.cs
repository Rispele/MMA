using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Sources.ServiceDefaults;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class Extensions
{
    private const string HealthEndpointPath = "/health";
    private const string AlivenessEndpointPath = "/alive";

    public static TBuilder ConfigurePostgresDbContextWithInstrumentation<TBuilder, TDbContext>(
        this TBuilder builder,
        string connectionName,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
        where TBuilder : IHostApplicationBuilder
        where TDbContext : DbContext
    {
        var connectionString = builder.Configuration.GetConnectionString(connectionName)
                               ?? throw new InvalidOperationException(
                                   $"Could not get connection string for connection: [{connectionName}]");

        builder.ConfigureInstrumentation<TDbContext>();
        builder.Services.ConfigurePostgresDbContext<TDbContext>(connectionString, npgsqlOptionsAction);

        return builder;
    }

    public static IServiceCollection ConfigurePostgresDbContext<TDbContext>(
        this IServiceCollection serviceCollection,
        string connectionString,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
        where TDbContext : DbContext
    {
        return serviceCollection.AddDbContextFactory<TDbContext>(optionsBuilder => optionsBuilder
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(connectionString, npgsqlOptionsAction));
    }

    private static void ConfigureInstrumentation<TContext>(this IHostApplicationBuilder builder)
        where TContext : DbContext
    {
        var healthCheckKey = $"Aspire.HealthChecks.{typeof(TContext).Name}";
        if (!builder.Properties.ContainsKey(healthCheckKey))
        {
            builder.Properties[healthCheckKey] = true;
            builder.Services.AddHealthChecks().AddDbContextCheck<TContext>();
        }

        builder.Services.AddOpenTelemetry().WithTracing(tracerProviderBuilder => { tracerProviderBuilder.AddNpgsql(); });

        double[] secondsBuckets = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10];
        builder.Services.AddOpenTelemetry().WithMetrics(meterProviderBuilder => meterProviderBuilder
            .AddMeter("Npgsql")
            .AddView(instrumentName: "db.client.commands.duration",
                new ExplicitBucketHistogramConfiguration { Boundaries = secondsBuckets })
            .AddView(instrumentName: "db.client.connections.create_time",
                new ExplicitBucketHistogramConfiguration { Boundaries = secondsBuckets }));
    }

    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();


        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        // Uncomment the following to restrict the allowed schemes for service discovery.
        // builder.Services.Configure<ServiceDiscoveryOptions>(options =>
        // {
        //     options.AllowedSchemes = ["https"];
        // });

        return builder;
    }

    public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddSource(builder.Environment.ApplicationName)
                    .AddAspNetCoreInstrumentation(tracing =>
                        // Exclude health check requests from tracing
                        tracing.Filter = context =>
                            !context.Request.Path.StartsWithSegments(HealthEndpointPath)
                            && !context.Request.Path.StartsWithSegments(AlivenessEndpointPath)
                    )
                    // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
                    //.AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        // Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
        //if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
        //{
        //    builder.Services.AddOpenTelemetry()
        //       .UseAzureMonitor();
        //}

        return builder;
    }

    public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        // Add a default liveness check to ensure app is responsive
        builder.Services.AddHealthChecks().AddCheck(name: "self", check: () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks(HealthEndpointPath);

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks(AlivenessEndpointPath, new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }

        return app;
    }
}
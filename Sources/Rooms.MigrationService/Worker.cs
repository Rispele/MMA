using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Rooms.Persistence;

namespace Rooms.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = ActivitySource.StartActivity(name: "Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();

            var roomsDbContext = scope.ServiceProvider.GetRequiredService<RoomsDbContext>();
            await RunMigrationAsync(roomsDbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => { await dbContext.Database.MigrateAsync(cancellationToken); });
    }
}
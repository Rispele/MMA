using FluentAssertions;
using IntegrationTestInfrastructure;
using IntegrationTestInfrastructure.ContainerBasedTests;
using Microsoft.EntityFrameworkCore;
using Rooms.Infrastructure.EFCore;

namespace Rooms.Migration.Tests.Tests;

[TestFixture]
[NonParallelizable]
public class RoomsMigrationTests : ContainerTestBase
{
    [Inject]
    private readonly RoomsDbContext dbContext = null!;

    [Test]
    public async Task ApplyAllMigrations_ShouldSucceed()
    {
        await dbContext.Database.EnsureDeletedAsync();
        
        var migration = () => dbContext.Database.MigrateAsync();

        await migration.Should().NotThrowAsync();
    }
}
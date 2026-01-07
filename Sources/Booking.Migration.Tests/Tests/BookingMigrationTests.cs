using Booking.Infrastructure.EFCore;
using FluentAssertions;
using Commons.Tests.Integration.Infrastructure;
using Commons.Tests.Integration.Infrastructure.ContainerBasedTests;
using Microsoft.EntityFrameworkCore;

namespace Booking.Migration.Tests.Tests;

[TestFixture]
[NonParallelizable]
public class BookingMigrationTests : ContainerTestBase
{
    [Inject]
    private readonly BookingDbContext dbContext = null!;

    [Test]
    public async Task ApplyAllMigrations_ShouldSucceed()
    {
        await dbContext.Database.EnsureDeletedAsync();

        var migration = () => dbContext.Database.MigrateAsync();

        await migration.Should().NotThrowAsync();
    }
}
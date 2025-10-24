using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Abstractions;
using Rooms.Domain.Queries.Implementations.Equipments;

namespace Rooms.Persistence.Factories;

public class DbContextModelsSourceFactory<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory) : IModelsSourceFactory 
    where TDbContext : DbContext, IModelsSource
{
    public async Task<IModelsSource> Create(CancellationToken cancellationToken = default)
    {
        return await dbContextFactory.CreateDbContextAsync(cancellationToken);
    }
}
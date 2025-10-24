using Microsoft.EntityFrameworkCore;
using Rooms.Domain.Queries.Abstractions;
using Rooms.Domain.Queries.Factories;

namespace Rooms.Persistence.Factories;

public class DbContextUnitOfWorkFactory<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory) : IUnitOfWorkFactory 
    where TDbContext : DbContext, IUnitOfWork
{
    public async Task<IUnitOfWork> Create(CancellationToken cancellationToken = default)
    {
        return await dbContextFactory.CreateDbContextAsync(cancellationToken);
    }
}
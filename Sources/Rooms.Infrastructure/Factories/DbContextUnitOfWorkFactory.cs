using MediatR;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;

namespace Rooms.Infrastructure.Factories;

public class DbContextUnitOfWorkFactory(IDbContextFactory<RoomsDbContext> dbContextFactory, IMediator mediator) : IUnitOfWorkFactory
{
    public async Task<IUnitOfWork> Create(CancellationToken cancellationToken = default)
    {
        return new RoomsDbContextUnitOfWork(await dbContextFactory.CreateDbContextAsync(cancellationToken), mediator);
    }
}
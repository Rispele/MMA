using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Rooms.Infrastructure.EFCore;

public class DbContextUnitOfWorkFactory(IDbContextFactory<RoomsDbContext> dbContextFactory, IMediator mediator) : IUnitOfWorkFactory
{
    public async Task<IUnitOfWork> Create(CancellationToken cancellationToken = default)
    {
        return new RoomsDbContextUnitOfWork(await dbContextFactory.CreateDbContextAsync(cancellationToken), mediator);
    }
}
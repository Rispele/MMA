using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore;

internal class BookingDbContextUnitOfWorkFactory(IDbContextFactory<BookingDbContext> dbContextFactory, IMediator mediator) : IUnitOfWorkFactory
{
    public async Task<IUnitOfWork> Create(CancellationToken cancellationToken = default)
    {
        return new BookingDbContextUnitOfWork(await dbContextFactory.CreateDbContextAsync(cancellationToken), mediator);
    }
}
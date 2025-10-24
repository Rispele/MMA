using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Factories;

public interface IUnitOfWorkFactory
{
    public Task<IUnitOfWork> Create(CancellationToken cancellationToken = default);
}
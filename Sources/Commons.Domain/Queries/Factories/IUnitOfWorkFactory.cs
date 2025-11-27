using Rooms.Core.Queries.Abstractions;

namespace Rooms.Core.Queries.Factories;

public interface IUnitOfWorkFactory
{
    public Task<IUnitOfWork> Create(CancellationToken cancellationToken = default);
}
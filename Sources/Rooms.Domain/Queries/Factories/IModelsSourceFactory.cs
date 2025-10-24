using Rooms.Domain.Queries.Abstractions;

namespace Rooms.Domain.Queries.Implementations.Equipments;

public interface IModelsSourceFactory
{
    public Task<IModelsSource> Create(CancellationToken cancellationToken = default);
}
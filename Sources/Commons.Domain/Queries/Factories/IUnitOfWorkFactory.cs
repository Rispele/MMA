using Commons.Domain.Queries.Abstractions;

namespace Commons.Domain.Queries.Factories;

public interface IUnitOfWorkFactory
{
    public Task<IUnitOfWork> Create(CancellationToken cancellationToken = default);
}
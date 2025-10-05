using Commons.Queries.Abstractions;
using Domain.Models;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations.Persistence;

public readonly struct FindFileByIdQuery(Guid fileId) : ISingleQueryObject<FileDescriptor?, DomainDbContext>
{
    public Task<FileDescriptor?> Apply(DomainDbContext dbContext, CancellationToken cancellationToken)
    {
        var id = fileId;

        return dbContext.Files.FirstOrDefaultAsync(t => t.Location.Id == id, cancellationToken: cancellationToken);
    }
}
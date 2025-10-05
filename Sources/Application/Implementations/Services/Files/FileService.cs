using Application.Implementations.Dtos.Files;
using Application.Implementations.Services.DtoConverters;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Implementations.Services.Files;

public class FileService(IDbContextFactory<DomainDbContext> domainDbContextProvider, FileDtoConverter fileDtoConverter) : IFileService
{
    public async Task<FileResultDto?> GetFileAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> StoreFileAsync(Stream content, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveFileAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }
}
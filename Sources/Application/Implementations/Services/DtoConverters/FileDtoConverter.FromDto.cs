using Application.Implementations.Dtos.Files;
using Domain.Models;

namespace Application.Implementations.Services.DtoConverters;

public partial class FileDtoConverter
{
    public FileDescriptor? Convert(FileMetadataDto? fileMetadata)
    {
        return fileMetadata == null ? null : new FileDescriptor(
            fileMetadata.FileName,
            Convert(fileMetadata.FileLocation)
        );
    }

    private static FileLocation Convert(FileLocationDto fileLocation)
    {
        return new FileLocation(fileLocation.Id, fileLocation.Bucket);
    }
}
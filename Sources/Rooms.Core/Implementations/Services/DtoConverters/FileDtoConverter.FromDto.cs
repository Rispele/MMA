using Rooms.Core.Implementations.Dtos.Files;
using Rooms.Domain.Models;

namespace Rooms.Core.Implementations.Services.DtoConverters;

public static partial class FileDtoConverter
{
    public static FileDescriptor? Convert(FileMetadataDto? fileMetadata)
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
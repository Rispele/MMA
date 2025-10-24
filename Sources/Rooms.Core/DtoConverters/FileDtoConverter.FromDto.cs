using Rooms.Core.Dtos.Files;
using Rooms.Domain.Models.File;

namespace Rooms.Core.DtoConverters;

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
using Rooms.Core.Implementations.Dtos.Files;
using Rooms.Domain.Models;

namespace Rooms.Core.Implementations.Services.DtoConverters;

public static class FileConvertExtensions
{
    public static FileMetadataDto ToDto(this FileDescriptor fileDescriptor)
    {
        return new FileMetadataDto(
            fileDescriptor.Filename,
            fileDescriptor.FileLocation.ToDto());
    }

    public static FileLocationDto ToDto(this FileLocation fileLocation)
    {
        return new FileLocationDto(fileLocation.Id, fileLocation.Bucket);
    }
}
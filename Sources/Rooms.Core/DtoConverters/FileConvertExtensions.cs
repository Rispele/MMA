using Rooms.Core.Dtos.Files;
using Rooms.Domain.Models.FileModels;

namespace Rooms.Core.DtoConverters;

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

    public static FileDescriptor FromDto(this FileMetadataDto fileDescriptor)
    {
        return new FileDescriptor(
            fileDescriptor.FileName,
            fileDescriptor.FileLocation.FromDto());
    }

    public static FileLocation FromDto(this FileLocationDto fileLocation)
    {
        return new FileLocation(fileLocation.Id, fileLocation.Bucket);
    }
}
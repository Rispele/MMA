using Rooms.Core.Dtos.Files;
using Rooms.Domain.Models.File;

namespace Rooms.Core.DtoConverters;

public static class FileConvertExtensions
{
    public static FileDescriptorDto ToDto(this FileDescriptor fileDescriptor)
    {
        return new FileDescriptorDto(
            fileDescriptor.Filename,
            fileDescriptor.Location.ToDto());
    }

    public static FileLocationDto ToDto(this FileLocation fileLocation)
    {
        return new FileLocationDto(fileLocation.Id, fileLocation.Bucket);
    }

    public static FileDescriptor FromDto(this FileDescriptorDto fileDescriptor)
    {
        return new FileDescriptor
        {
            Filename = fileDescriptor.FileName,
            Location = fileDescriptor.Location.FromDto(),
        };
    }

    public static FileLocation FromDto(this FileLocationDto fileLocation)
    {
        return new FileLocation
        {
            Id = fileLocation.Id,
            Bucket = fileLocation.Bucket,
        };
    }
}
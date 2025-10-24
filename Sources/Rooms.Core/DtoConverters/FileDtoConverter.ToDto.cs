using Rooms.Core.Dtos.Files;
using Rooms.Domain.Models.File;

namespace Rooms.Core.DtoConverters;

public static partial class FileDtoConverter
{
    public static FileDescriptorDto Convert(FileDescriptor fileDescriptor)
    {
        return new FileDescriptorDto(
            fileDescriptor.Filename,
            FileLocation: Convert(fileDescriptor.FileLocation)
        );
    }

    public static FileLocationDto Convert(FileLocation fileLocation)
    {
        return new FileLocationDto(fileLocation.Id, fileLocation.Bucket);
    }
}
using Commons;
using Rooms.Core.Dtos.Files;
using Rooms.Domain.Models.File;

namespace Rooms.Core.DtoConverters;

public static partial class FileDtoConverter
{
    public static FileDescriptor? Convert(FileDescriptorDto? fileMetadata)
    {
        return fileMetadata == null
            ? null
            : new FileDescriptor
            {
                Filename = fileMetadata.FileName,
                Location = fileMetadata.Location.Map(Convert)
            };
    }

    public static FileLocation Convert(FileLocationDto fileLocation)
    {
        return new FileLocation
        {
            Id = fileLocation.Id,
            Bucket = fileLocation.Bucket,
        };
    }
}
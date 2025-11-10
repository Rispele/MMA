using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.Files;
using Rooms.Domain.Models.File;

namespace Rooms.Core.DtoConverters;

[Mapper]
public static partial class FileDtoConverter
{
    public static partial FileDescriptorDto Convert(FileDescriptor fileDescriptor);

    public static partial FileDescriptor? Convert(FileDescriptorDto? fileMetadata);

    public static partial FileLocation Convert(FileLocationDto fileLocation);
}
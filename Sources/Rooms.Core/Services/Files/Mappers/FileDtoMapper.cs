using Riok.Mapperly.Abstractions;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Domain.Models.File;

namespace Rooms.Core.Services.Files.Mappers;

[Mapper]
public static partial class FileDtoMapper
{
    public static partial FileDescriptorDto Convert(FileDescriptor fileDescriptor);

    public static partial FileDescriptor? Convert(FileDescriptorDto? fileMetadata);

    public static partial FileLocation Convert(FileLocationDto fileLocation);
}
using Application.Implementations.Dtos;
using Domain.Models;

namespace Application.Implementations.Services.DtoConverters;

public static class FileConvertExtensions
{
    public static FileDto ToDto(this Domain.Models.FileDescriptor fileDescriptor)
    {
        return new FileDto(
            fileDescriptor.Filename,
            fileDescriptor.Location.ToDto());
    }

    public static LocationDto ToDto(this Location location)
    {
        return new LocationDto(location.Id, location.Bucket);
    }
}
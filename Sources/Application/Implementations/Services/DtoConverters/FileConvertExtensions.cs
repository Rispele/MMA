using Application.Implementations.Dtos;
using Domain.Models;

namespace Application.Implementations.Services.DtoConverters;

public static class FileConvertExtensions
{
    public static FileDto ToDto(this Domain.Models.File file)
    {
        return new FileDto(
            file.Filename,
            file.Location.ToDto());
    }

    public static LocationDto ToDto(this Location location)
    {
        return new LocationDto(location.Id, location.Bucket);
    }
}
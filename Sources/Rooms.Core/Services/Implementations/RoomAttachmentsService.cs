using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Services;

namespace Rooms.Core.Services.Implementations;

public class RoomAttachmentsService(IObjectStorageService objectStorageService) : IRoomAttachmentsService
{
    private const string AttachmentsBucket = "attachments";
    private const int AttachmentsExpirationInSeconds = 300;

    public async Task<TempFileUrlDto?> Load(FileLocationDto fileLocation)
    {
        var data = await objectStorageService.FindObject(
            location: FileDtoConverter.Convert(fileLocation),
            AttachmentsExpirationInSeconds);

        return new TempFileUrlDto { Url = data.Url };
    }

    public async Task<FileDescriptorDto> Store(
        Guid id,
        string fileName, 
        Stream content, 
        CancellationToken cancellationToken)
    {
        var fileDescriptor = await objectStorageService.StoreObject(
            id,
            AttachmentsBucket,
            fileName,
            content,
            cancellationToken);
        
        return FileDtoConverter.Convert(fileDescriptor);
    }

    public Task Remove(FileLocationDto fileLocation, CancellationToken cancellationToken)
    {
        return objectStorageService.Remove(FileDtoConverter.Convert(fileLocation), cancellationToken);
    }
}
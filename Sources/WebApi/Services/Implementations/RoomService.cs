using WebApi.Models;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class RoomService(IFileService fileService) : IRoomService
{
    public Task<RoomsResponse> GetRoomsAsync(RoomsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomModel?> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomModel?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<RoomModel> CreateRoomAsync(PostRoomRequest request, CancellationToken cancellationToken)
    {
        var pdfRoomSchemeFile = request is { PdfRoomSchemeFileContent: not null, PdfRoomSchemeFileName: not null }
            ? await fileService.StoreFileAsync(new MemoryStream(request.PdfRoomSchemeFileContent!), cancellationToken)
            : null;
        var pdfRoomSchemeFileModel = pdfRoomSchemeFile != null
            ? new FileModel(request.PdfRoomSchemeFileName!,
                new FileLocationModel(pdfRoomSchemeFile.Id, pdfRoomSchemeFile.BucketName))
            : null;
        var photoFile = request is { PhotoFileContent: not null, PhotoFileName: not null }
            ? await fileService.StoreFileAsync(new MemoryStream(request.PhotoFileContent!), cancellationToken)
            : null;
        var photoFileModel = photoFile != null
            ? new FileModel(request.PhotoFileName!,
                new FileLocationModel(photoFile.Id, photoFile.BucketName))
            : null;

        var model = new RoomModel
        {
            Attachments = pdfRoomSchemeFileModel != null && photoFileModel != null
                ? new RoomAttachmentsModel(pdfRoomSchemeFileModel, photoFileModel)
                : null,
        };

        return model;
    }

    public Task<PatchRoomModel?> GetPatchModelAsync(int roomId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<RoomModel> ApplyPatchAndSaveAsync(int roomId, PatchRoomModel patchedModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
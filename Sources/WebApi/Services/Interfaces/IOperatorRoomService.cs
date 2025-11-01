using WebApi.Models.OperatorRoom;
using WebApi.Models.Requests.OperatorRooms;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IOperatorRoomService
{
    Task<OperatorRoomsResponseModel> GetOperatorRoomsAsync(GetOperatorRoomsModel model, CancellationToken cancellationToken);
    Task<OperatorRoomModel> GetOperatorRoomByIdAsync(int id, CancellationToken cancellationToken);
    Task<OperatorRoomModel> CreateOperatorRoomAsync(CreateOperatorRoomModel model, CancellationToken cancellationToken);
    Task<PatchOperatorRoomModel> GetOperatorRoomPatchModel(int operatorRoomId, CancellationToken cancellationToken);

    Task<OperatorRoomModel> PatchOperatorRoomAsync(
        int operatorRoomId,
        PatchOperatorRoomModel request,
        CancellationToken cancellationToken);
}
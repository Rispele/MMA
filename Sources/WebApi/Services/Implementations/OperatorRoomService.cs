using WebApi.Models.OperatorRoom;
using WebApi.Models.Requests.OperatorRooms;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using OperatorRoomsModelsConverter = WebApi.ModelConverters.OperatorRoomsModelsConverter;
using ICoreOperatorRoomService = Rooms.Core.Services.Interfaces.IOperatorRoomService;

namespace WebApi.Services.Implementations;

public class OperatorRoomService(ICoreOperatorRoomService operatorRoomService) : IOperatorRoomService
{
    public async Task<OperatorRoomsResponseModel> GetOperatorRoomsAsync(
        GetOperatorRoomsModel model,
        CancellationToken cancellationToken)
    {
        var getOperatorRoomsRequest = OperatorRoomsModelsConverter.Convert(model);

        var batch = await operatorRoomService.FilterOperatorRooms(getOperatorRoomsRequest, cancellationToken);

        return new OperatorRoomsResponseModel
        {
            OperatorRooms = batch.OperatorRooms.Select(OperatorRoomsModelsConverter.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<OperatorRoomModel> GetOperatorRoomByIdAsync(int id, CancellationToken cancellationToken)
    {
        var operatorRoom = await operatorRoomService.GetOperatorRoomById(id, cancellationToken);

        return OperatorRoomsModelsConverter.Convert(operatorRoom);
    }

    public async Task<Dictionary<Guid, string>> GetAvailableOperatorsAsync(CancellationToken cancellationToken)
    {
        var operators = await operatorRoomService.GetAvailableOperators(cancellationToken);

        return operators;
    }

    public async Task<OperatorRoomModel> CreateOperatorRoomAsync(
        CreateOperatorRoomModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = OperatorRoomsModelsConverter.Convert(model);

        var operatorRoom = await operatorRoomService.CreateOperatorRoom(innerRequest, cancellationToken);

        return OperatorRoomsModelsConverter.Convert(operatorRoom);
    }

    public async Task<PatchOperatorRoomModel> GetOperatorRoomPatchModel(int operatorRoomId, CancellationToken cancellationToken)
    {
        var operatorRoom = await operatorRoomService.GetOperatorRoomById(operatorRoomId, cancellationToken);

        return OperatorRoomsModelsConverter.ConvertToPatchModel(operatorRoom);
    }

    public async Task<OperatorRoomModel> PatchOperatorRoomAsync(
        int operatorRoomId,
        PatchOperatorRoomModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = OperatorRoomsModelsConverter.Convert(patchModel);

        var patched = await operatorRoomService.PatchOperatorRoom(operatorRoomId, patchRequest, cancellationToken);

        return OperatorRoomsModelsConverter.Convert(patched);
    }
}
using WebApi.Models.Equipment;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using ICoreEquipmentService = Rooms.Core.Implementations.Services.Equipments.IEquipmentService;

namespace WebApi.Services.Implementations;

public class EquipmentService(ICoreEquipmentService equipmentService) : IEquipmentService
{
    public async Task<EquipmentsResponse> GetEquipmentsAsync(EquipmentsRequest request, CancellationToken cancellationToken)
    {
        var getEquipmentsRequest = EquipmentsModelsConverter.Convert(request);

        var batch = await equipmentService.FilterEquipments(getEquipmentsRequest, cancellationToken);

        return new EquipmentsResponse
        {
            Equipments = batch.Equipments.Select(EquipmentsModelsConverter.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(id, cancellationToken);

        return EquipmentsModelsConverter.Convert(equipment);
    }

    public async Task<EquipmentModel> CreateEquipmentAsync(CreateEquipmentModel model, CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentsModelsConverter.Convert(model);

        var equipment = await equipmentService.CreateEquipment(innerRequest, cancellationToken);

        return EquipmentsModelsConverter.Convert(equipment);
    }

    public async Task<PatchEquipmentModel> GetPatchModel(int equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(equipmentId, cancellationToken);

        return EquipmentsModelsConverter.ConvertToPatchModel(equipment);
    }

    public async Task<EquipmentModel> PatchEquipmentAsync(int equipmentId, PatchEquipmentModel patchModel, CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentsModelsConverter.Convert(patchModel);

        var patched = await equipmentService.PatchEquipment(equipmentId, patchRequest, cancellationToken);

        return EquipmentsModelsConverter.Convert(patched);
    }
}
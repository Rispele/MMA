using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using EquipmentsModelsConverter = WebApi.ModelConverters.EquipmentsModelsConverter;
using ICoreEquipmentService = Rooms.Core.Services.Interfaces.IEquipmentService;

namespace WebApi.Services.Implementations;

public class EquipmentService(ICoreEquipmentService equipmentService) : IEquipmentService
{
    public async Task<EquipmentsResponseModel> GetEquipmentsAsync(
        GetEquipmentsModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentsRequest = EquipmentsModelsConverter.Convert(model);

        var batch = await equipmentService.FilterEquipments(getEquipmentsRequest, cancellationToken);

        return new EquipmentsResponseModel
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

    public async Task<EquipmentModel> CreateEquipmentAsync(
        CreateEquipmentModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentsModelsConverter.Convert(model);

        var equipment = await equipmentService.CreateEquipment(innerRequest, cancellationToken);

        return EquipmentsModelsConverter.Convert(equipment);
    }

    public async Task<PatchEquipmentModel> GetEquipmentPatchModel(int equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(equipmentId, cancellationToken);

        return EquipmentsModelsConverter.ConvertToPatchModel(equipment);
    }

    public async Task<EquipmentModel> PatchEquipmentAsync(
        int equipmentId,
        PatchEquipmentModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentsModelsConverter.Convert(patchModel);

        var patched = await equipmentService.PatchEquipment(equipmentId, patchRequest, cancellationToken);

        return EquipmentsModelsConverter.Convert(patched);
    }

    public async Task<FileExportModel> ExportEquipmentRegistry(CancellationToken cancellationToken)
    {
        var fileData = await equipmentService.ExportEquipmentRegistry(cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            Content = fileData.Content,
            ContentType = fileData.ContentType,
        };
    }
}
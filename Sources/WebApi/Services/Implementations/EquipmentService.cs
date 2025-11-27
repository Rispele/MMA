using Rooms.Core.Interfaces.Services.Spreadsheets;
using WebApi.ModelConverters;
using WebApi.Models.Equipment;
using WebApi.Models.Files;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class EquipmentService(
    Rooms.Core.Interfaces.Services.Equipments.IEquipmentService equipmentService,
    ISpreadsheetService spreadsheetService) : IEquipmentService
{
    public async Task<EquipmentsResponseModel> GetEquipmentsAsync(
        GetEquipmentsModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentsRequest = EquipmentModelsMapper.MapGetEquipmentFromModel(model);

        var batch = await equipmentService.FilterEquipments(getEquipmentsRequest, cancellationToken);

        return new EquipmentsResponseModel
        {
            Equipments = batch.Equipments.Select(EquipmentModelsMapper.MapEquipmentToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(id, cancellationToken);

        return EquipmentModelsMapper.MapEquipmentToModel(equipment);
    }

    public async Task<EquipmentModel> CreateEquipmentAsync(
        CreateEquipmentModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentModelsMapper.MapCreateEquipmentFromModel(model);

        var equipment = await equipmentService.CreateEquipment(innerRequest, cancellationToken);

        return EquipmentModelsMapper.MapEquipmentToModel(equipment);
    }

    public async Task<PatchEquipmentModel> GetEquipmentPatchModel(int equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(equipmentId, cancellationToken);

        return EquipmentModelsMapper.MapEquipmentToPatchModel(equipment);
    }

    public async Task<EquipmentModel> PatchEquipmentAsync(
        int equipmentId,
        PatchEquipmentModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentModelsMapper.MapPatchEquipmentFromModel(patchModel);

        var patched = await equipmentService.PatchEquipment(equipmentId, patchRequest, cancellationToken);

        return EquipmentModelsMapper.MapEquipmentToModel(patched);
    }

    public async Task<FileExportModel> ExportEquipmentRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var fileData = await spreadsheetService.ExportEquipmentRegistry(outputStream, cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            ContentType = fileData.ContentType,
            Flush = fileData.Flush,
        };
    }
}
using Rooms.Core.Interfaces.Services.Spreadsheets;
using WebApi.Core.ModelConverters;
using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Equipments;
using WebApi.Core.Models.Responses;
using WebApi.Core.Services.Interfaces;

namespace WebApi.Core.Services.Implementations;

public class EquipmentService(
    Rooms.Core.Interfaces.Services.Equipments.IEquipmentService equipmentService,
    ISpreadsheetService spreadsheetService) : IEquipmentService
{
    public async Task<EquipmentsResponseModel> GetEquipmentsAsync(
        GetRequest<EquipmentsFilterModel> model,
        CancellationToken cancellationToken)
    {
        var getEquipmentsRequest = EquipmentModelsMapper.MapGetEquipmentFromModel(model);

        var batch = await equipmentService.FilterEquipments(getEquipmentsRequest, cancellationToken);

        return new EquipmentsResponseModel(
            batch.Equipments.Select(EquipmentModelsMapper.MapEquipmentToModel).ToArray(),
            batch.TotalCount);
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
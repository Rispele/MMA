using WebApi.ModelConverters;
using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using ICoreEquipmentService = Rooms.Core.Services.Interfaces.IEquipmentService;

namespace WebApi.Services.Implementations;

public class EquipmentService(ICoreEquipmentService equipmentService) : IEquipmentService
{
    public async Task<EquipmentsResponseModel> GetEquipmentsAsync(
        GetEquipmentsModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentsRequest = EquipmentModelConverter.Convert(model);

        var batch = await equipmentService.FilterEquipments(getEquipmentsRequest, cancellationToken);

        return new EquipmentsResponseModel
        {
            Equipments = batch.Equipments.Select(EquipmentModelMapper.MapEquipmentToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(id, cancellationToken);

        return EquipmentModelMapper.MapEquipmentToModel(equipment);
    }

    public async Task<EquipmentModel> CreateEquipmentAsync(
        CreateEquipmentModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentModelMapper.MapCreateEquipmentFromModel(model);

        var equipment = await equipmentService.CreateEquipment(innerRequest, cancellationToken);

        return EquipmentModelMapper.MapEquipmentToModel(equipment);
    }

    public async Task<PatchEquipmentModel> GetEquipmentPatchModel(int equipmentId, CancellationToken cancellationToken)
    {
        var equipment = await equipmentService.GetEquipmentById(equipmentId, cancellationToken);

        return EquipmentModelMapper.MapEquipmentToPatchModel(equipment);
    }

    public async Task<EquipmentModel> PatchEquipmentAsync(
        int equipmentId,
        PatchEquipmentModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentModelMapper.MapPatchEquipmentTypeFromModel(patchModel);

        var patched = await equipmentService.PatchEquipment(equipmentId, patchRequest, cancellationToken);

        return EquipmentModelMapper.MapEquipmentToModel(patched);
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
using WebApi.ModelConverters;
using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using ICoreEquipmentTypeService = Rooms.Core.Services.Interfaces.IEquipmentTypeService;

namespace WebApi.Services.Implementations;

public class EquipmentTypeService(ICoreEquipmentTypeService equipmentTypeService) : IEquipmentTypeService
{
    public async Task<EquipmentTypesResponseModel> GetEquipmentTypesAsync(
        GetEquipmentTypesModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentTypesRequest = EquipmentTypeModelConverter.Convert(model);

        var batch = await equipmentTypeService.FilterEquipmentTypes(getEquipmentTypesRequest, cancellationToken);

        return new EquipmentTypesResponseModel
        {
            EquipmentTypes = batch.EquipmentTypes.Select(EquipmentTypeModelMapper.MapEquipmentTypeToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentTypeModel> GetEquipmentTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(id, cancellationToken);

        return EquipmentTypeModelMapper.MapEquipmentTypeToModel(equipmentType);
    }

    public async Task<EquipmentTypeModel> CreateEquipmentTypeAsync(
        CreateEquipmentTypeModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentTypeModelConverter.Convert(model);

        var equipmentType = await equipmentTypeService.CreateEquipmentType(innerRequest, cancellationToken);

        return EquipmentTypeModelMapper.MapEquipmentTypeToModel(equipmentType);
    }

    public async Task<PatchEquipmentTypeModel> GetEquipmentTypePatchModel(int equipmentTypeId, CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(equipmentTypeId, cancellationToken);

        return EquipmentTypeModelMapper.MapEquipmentTypeToPatchModel(equipmentType);
    }

    public async Task<EquipmentTypeModel> PatchEquipmentTypeAsync(
        int equipmentTypeId,
        PatchEquipmentTypeModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentTypeModelConverter.Convert(patchModel);

        var patched = await equipmentTypeService.PatchEquipmentType(equipmentTypeId, patchRequest, cancellationToken);

        return EquipmentTypeModelMapper.MapEquipmentTypeToModel(patched);
    }

    public async Task<FileExportModel> ExportEquipmentTypeRegistry(CancellationToken cancellationToken)
    {
        var fileData = await equipmentTypeService.ExportEquipmentTypeRegistry(cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            Content = fileData.Content,
            ContentType = fileData.ContentType,
        };
    }
}
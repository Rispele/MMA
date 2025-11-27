using Rooms.Core.Services.Spreadsheets;
using WebApi.ModelConverters;
using WebApi.Models.Equipment;
using WebApi.Models.Files;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class EquipmentTypeService(
    Rooms.Core.Interfaces.Services.Equipments.IEquipmentTypeService equipmentTypeService,
    SpreadsheetService spreadsheetService) : IEquipmentTypeService
{
    public async Task<EquipmentTypesResponseModel> GetEquipmentTypesAsync(
        GetEquipmentTypesModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentTypesRequest = EquipmentTypeModelsMapper.MapGetEquipmentTypesFromModel(model);

        var batch = await equipmentTypeService.FilterEquipmentTypes(getEquipmentTypesRequest, cancellationToken);

        return new EquipmentTypesResponseModel
        {
            EquipmentTypes = batch.EquipmentTypes.Select(EquipmentTypeModelsMapper.MapEquipmentTypeToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentTypeModel> GetEquipmentTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(id, cancellationToken);

        return EquipmentTypeModelsMapper.MapEquipmentTypeToModel(equipmentType);
    }

    public async Task<EquipmentTypeModel> CreateEquipmentTypeAsync(
        CreateEquipmentTypeModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentTypeModelsMapper.MapCreateEquipmentTypeFromModel(model);

        var equipmentType = await equipmentTypeService.CreateEquipmentType(innerRequest, cancellationToken);

        return EquipmentTypeModelsMapper.MapEquipmentTypeToModel(equipmentType);
    }

    public async Task<PatchEquipmentTypeModel> GetEquipmentTypePatchModel(int equipmentTypeId, CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(equipmentTypeId, cancellationToken);

        return EquipmentTypeModelsMapper.MapEquipmentTypeToPatchModel(equipmentType);
    }

    public async Task<EquipmentTypeModel> PatchEquipmentTypeAsync(
        int equipmentTypeId,
        PatchEquipmentTypeModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentTypeModelsMapper.MapPatchEquipmentTypeFromModel(patchModel);

        var patched = await equipmentTypeService.PatchEquipmentType(equipmentTypeId, patchRequest, cancellationToken);

        return EquipmentTypeModelsMapper.MapEquipmentTypeToModel(patched);
    }

    public async Task<FileExportModel> ExportEquipmentTypeRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var fileData = await spreadsheetService.ExportEquipmentTypeRegistry(outputStream, cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            ContentType = fileData.ContentType,
            Flush = fileData.Flush,
        };
    }
}
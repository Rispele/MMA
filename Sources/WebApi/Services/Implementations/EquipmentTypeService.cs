using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using EquipmentTypesModelsConverter = WebApi.ModelConverters.EquipmentTypesModelsConverter;
using ICoreEquipmentTypeService = Rooms.Core.Services.Interfaces.IEquipmentTypeService;

namespace WebApi.Services.Implementations;

public class EquipmentTypeService(ICoreEquipmentTypeService equipmentTypeService) : IEquipmentTypeService
{
    public async Task<EquipmentTypesResponseModel> GetEquipmentTypesAsync(
        GetEquipmentTypesModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentTypesRequest = EquipmentTypesModelsConverter.Convert(model);

        var batch = await equipmentTypeService.FilterEquipmentTypes(getEquipmentTypesRequest, cancellationToken);

        return new EquipmentTypesResponseModel
        {
            EquipmentTypes = batch.EquipmentTypes.Select(EquipmentTypesModelsConverter.Convert).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentTypeModel> GetEquipmentTypeByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(id, cancellationToken);

        return EquipmentTypesModelsConverter.Convert(equipmentType);
    }

    public async Task<EquipmentTypeModel> CreateEquipmentTypeAsync(
        CreateEquipmentTypeModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentTypesModelsConverter.Convert(model);

        var equipmentType = await equipmentTypeService.CreateEquipmentType(innerRequest, cancellationToken);

        return EquipmentTypesModelsConverter.Convert(equipmentType);
    }

    public async Task<PatchEquipmentTypeModel> GetEquipmentTypePatchModel(int equipmentTypeId, CancellationToken cancellationToken)
    {
        var equipmentType = await equipmentTypeService.GetEquipmentTypeById(equipmentTypeId, cancellationToken);

        return EquipmentTypesModelsConverter.ConvertToPatchModel(equipmentType);
    }

    public async Task<EquipmentTypeModel> PatchEquipmentTypeAsync(
        int equipmentTypeId,
        PatchEquipmentTypeModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentTypesModelsConverter.Convert(patchModel);

        var patched = await equipmentTypeService.PatchEquipmentType(equipmentTypeId, patchRequest, cancellationToken);

        return EquipmentTypesModelsConverter.Convert(patched);
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
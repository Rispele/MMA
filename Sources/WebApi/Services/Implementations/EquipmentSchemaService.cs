using WebApi.ModelConverters;
using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using ICoreEquipmentSchemaService = Rooms.Core.Services.Interfaces.IEquipmentSchemaService;

namespace WebApi.Services.Implementations;

public class EquipmentSchemaService(ICoreEquipmentSchemaService equipmentSchemaService) : IEquipmentSchemaService
{
    public async Task<EquipmentSchemasResponseModel> GetEquipmentSchemasAsync(
        GetEquipmentSchemasModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentSchemasRequest = EquipmentSchemaModelConverter.Convert(model);

        var batch = await equipmentSchemaService.FilterEquipmentSchemas(getEquipmentSchemasRequest, cancellationToken);

        return new EquipmentSchemasResponseModel
        {
            EquipmentSchemas = batch.EquipmentSchemas.Select(EquipmentSchemaModelMapper.MapEquipmentSchemaToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentSchemaModel> GetEquipmentSchemaByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaById(id, cancellationToken);

        return EquipmentSchemaModelMapper.MapEquipmentSchemaToModel(equipmentSchema);
    }

    public async Task<EquipmentSchemaModel> CreateEquipmentSchemaAsync(
        CreateEquipmentSchemaModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentSchemaModelConverter.Convert(model);

        var equipmentSchema = await equipmentSchemaService.CreateEquipmentSchema(innerRequest, cancellationToken);

        return EquipmentSchemaModelMapper.MapEquipmentSchemaToModel(equipmentSchema);
    }

    public async Task<PatchEquipmentSchemaModel> GetEquipmentSchemaPatchModel(int equipmentSchemaId, CancellationToken cancellationToken)
    {
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaById(equipmentSchemaId, cancellationToken);

        return EquipmentSchemaModelMapper.MapEquipmentSchemaToPatchModel(equipmentSchema);
    }

    public async Task<EquipmentSchemaModel> PatchEquipmentSchemaAsync(
        int equipmentSchemaId,
        PatchEquipmentSchemaModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentSchemaModelConverter.Convert(patchModel);

        var patched = await equipmentSchemaService.PatchEquipmentSchema(equipmentSchemaId, patchRequest, cancellationToken);

        return EquipmentSchemaModelMapper.MapEquipmentSchemaToModel(patched);
    }

    public async Task<FileExportModel> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken)
    {
        var fileData = await equipmentSchemaService.ExportEquipmentSchemaRegistry(cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            Content = fileData.Content,
            ContentType = fileData.ContentType,
        };
    }
}
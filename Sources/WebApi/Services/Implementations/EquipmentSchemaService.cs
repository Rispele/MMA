using Rooms.Core.Spreadsheets;
using WebApi.ModelConverters;
using WebApi.Models.Equipment;
using WebApi.Models.Files;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;
using ICoreEquipmentSchemaService = Rooms.Core.Services.Interfaces.IEquipmentSchemaService;

namespace WebApi.Services.Implementations;

public class EquipmentSchemaService(ICoreEquipmentSchemaService equipmentSchemaService,
    SpreadsheetService spreadsheetService) : IEquipmentSchemaService
{
    public async Task<EquipmentSchemasResponseModel> GetEquipmentSchemasAsync(
        GetEquipmentSchemasModel model,
        CancellationToken cancellationToken)
    {
        var getEquipmentSchemasRequest = EquipmentSchemaModelsMapper.MapGetEquipmentSchemaFromModel(model);

        var batch = await equipmentSchemaService.FilterEquipmentSchemas(getEquipmentSchemasRequest, cancellationToken);

        return new EquipmentSchemasResponseModel
        {
            EquipmentSchemas = batch.EquipmentSchemas.Select(EquipmentSchemaModelsMapper.MapEquipmentSchemaToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<EquipmentSchemaModel> GetEquipmentSchemaByIdAsync(int id, CancellationToken cancellationToken)
    {
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaById(id, cancellationToken);

        return EquipmentSchemaModelsMapper.MapEquipmentSchemaToModel(equipmentSchema);
    }

    public async Task<EquipmentSchemaModel> CreateEquipmentSchemaAsync(
        CreateEquipmentSchemaModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = EquipmentSchemaModelsMapper.MapCreateEquipmentSchemaFromModel(model);

        var equipmentSchema = await equipmentSchemaService.CreateEquipmentSchema(innerRequest, cancellationToken);

        return EquipmentSchemaModelsMapper.MapEquipmentSchemaToModel(equipmentSchema);
    }

    public async Task<PatchEquipmentSchemaModel> GetEquipmentSchemaPatchModel(int equipmentSchemaId, CancellationToken cancellationToken)
    {
        var equipmentSchema = await equipmentSchemaService.GetEquipmentSchemaById(equipmentSchemaId, cancellationToken);

        return EquipmentSchemaModelsMapper.MapEquipmentSchemaToPatchModel(equipmentSchema);
    }

    public async Task<EquipmentSchemaModel> PatchEquipmentSchemaAsync(
        int equipmentSchemaId,
        PatchEquipmentSchemaModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = EquipmentSchemaModelsMapper.MapPatchEquipmentSchemaFromModel(patchModel);

        var patched = await equipmentSchemaService.PatchEquipmentSchema(equipmentSchemaId, patchRequest, cancellationToken);

        return EquipmentSchemaModelsMapper.MapEquipmentSchemaToModel(patched);
    }

    public async Task<FileExportModel> ExportEquipmentSchemaRegistry(Stream outputStream, CancellationToken cancellationToken)
    {
        var fileData = await spreadsheetService.ExportEquipmentSchemaRegistry(outputStream, cancellationToken);
        return new FileExportModel
        {
            FileName = fileData.FileName,
            ContentType = fileData.ContentType,
            Flush = fileData.Flush,
        };
    }
}
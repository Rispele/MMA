using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IEquipmentSchemaService
{
    Task<EquipmentSchemasResponseModel> GetEquipmentSchemasAsync(GetEquipmentSchemasModel model, CancellationToken cancellationToken);
    Task<EquipmentSchemaModel> GetEquipmentSchemaByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentSchemaModel> CreateEquipmentSchemaAsync(CreateEquipmentSchemaModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentSchemaModel> GetEquipmentSchemaPatchModel(int equipmentSchemaId, CancellationToken cancellationToken);
    Task<EquipmentSchemaModel> PatchEquipmentSchemaAsync(
        int equipmentSchemaId,
        PatchEquipmentSchemaModel request,
        CancellationToken cancellationToken);
    Task<FileExportModel> ExportEquipmentSchemaRegistry(CancellationToken cancellationToken);
}
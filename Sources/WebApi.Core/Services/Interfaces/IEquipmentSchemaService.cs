using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.EquipmentSchemas;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IEquipmentSchemaService
{
    Task<EquipmentSchemasResponseModel> GetEquipmentSchemasAsync(GetRequest<EquipmentSchemasFilterModel> model, CancellationToken cancellationToken);
    Task<EquipmentSchemaModel> GetEquipmentSchemaByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentSchemaModel> CreateEquipmentSchemaAsync(CreateEquipmentSchemaModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentSchemaModel> GetEquipmentSchemaPatchModel(int equipmentSchemaId, CancellationToken cancellationToken);

    Task<EquipmentSchemaModel> PatchEquipmentSchemaAsync(
        int equipmentSchemaId,
        PatchEquipmentSchemaModel request,
        CancellationToken cancellationToken);

    Task<FileExportModel> ExportEquipmentSchemaRegistry(Stream outputStream, CancellationToken cancellationToken);
}
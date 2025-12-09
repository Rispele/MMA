using WebApi.Models.Equipment;
using WebApi.Models.Files;
using WebApi.Models.Requests;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IEquipmentTypeService
{
    Task<EquipmentTypesResponseModel> GetEquipmentTypesAsync(GetRequest<EquipmentTypesFilterModel> model, CancellationToken cancellationToken);
    Task<EquipmentTypeModel> GetEquipmentTypeByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentTypeModel> CreateEquipmentTypeAsync(CreateEquipmentTypeModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentTypeModel> GetEquipmentTypePatchModel(int equipmentTypeId, CancellationToken cancellationToken);

    Task<EquipmentTypeModel> PatchEquipmentTypeAsync(
        int equipmentTypeId,
        PatchEquipmentTypeModel request,
        CancellationToken cancellationToken);

    Task<FileExportModel> ExportEquipmentTypeRegistry(Stream outputStream, CancellationToken cancellationToken);
}
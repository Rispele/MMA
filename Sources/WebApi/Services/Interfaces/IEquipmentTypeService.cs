using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IEquipmentTypeService
{
    Task<EquipmentTypesResponseModel> GetEquipmentTypesAsync(GetEquipmentTypesModel model, CancellationToken cancellationToken);
    Task<EquipmentTypeModel> GetEquipmentTypeByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentTypeModel> CreateEquipmentTypeAsync(CreateEquipmentTypeModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentTypeModel> GetEquipmentTypePatchModel(int equipmentTypeId, CancellationToken cancellationToken);
    Task<EquipmentTypeModel> PatchEquipmentTypeAsync(
        int equipmentTypeId,
        PatchEquipmentTypeModel request,
        CancellationToken cancellationToken);
    Task<FileExportModel> ExportEquipmentTypeRegistry(CancellationToken cancellationToken);
}
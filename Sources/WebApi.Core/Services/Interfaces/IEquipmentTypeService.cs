using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.EquipmentTypes;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

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
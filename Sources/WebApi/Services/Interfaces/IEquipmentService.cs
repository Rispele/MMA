using WebApi.Models;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IEquipmentService
{
    Task<EquipmentsResponseModel> GetEquipmentsAsync(GetEquipmentsModel model, CancellationToken cancellationToken);
    Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentModel> CreateEquipmentAsync(CreateEquipmentModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentModel> GetEquipmentPatchModel(int equipmentId, CancellationToken cancellationToken);

    Task<EquipmentModel> PatchEquipmentAsync(
        int equipmentId,
        PatchEquipmentModel request,
        CancellationToken cancellationToken);

    Task<FileExportModel> ExportEquipmentRegistry(CancellationToken cancellationToken);
}
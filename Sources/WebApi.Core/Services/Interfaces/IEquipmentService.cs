using WebApi.Core.Models.Equipment;
using WebApi.Core.Models.Files;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.Equipments;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IEquipmentService
{
    Task<EquipmentsResponseModel> GetEquipmentsAsync(GetRequest<EquipmentsFilterModel> model, CancellationToken cancellationToken);
    Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentModel[]> CreateEquipmentAsync(CreateEquipmentModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentModel> GetEquipmentPatchModel(int equipmentId, CancellationToken cancellationToken);

    Task<EquipmentModel> PatchEquipmentAsync(
        int equipmentId,
        PatchEquipmentModel request,
        CancellationToken cancellationToken);

    Task<FileExportModel> ExportEquipmentRegistry(Stream outputStream, CancellationToken cancellationToken);
}
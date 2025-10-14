using WebApi.Models.Equipment;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IEquipmentService
{
    Task<EquipmentsResponse> GetEquipmentsAsync(EquipmentsRequest request, CancellationToken cancellationToken);
    Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken);
    Task<EquipmentModel> CreateEquipmentAsync(CreateEquipmentModel model, CancellationToken cancellationToken);
    Task<PatchEquipmentModel> GetPatchModel(int equipmentId, CancellationToken cancellationToken);
    Task<EquipmentModel> PatchEquipmentAsync(int equipmentId, PatchEquipmentModel request, CancellationToken cancellationToken);
}
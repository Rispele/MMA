using WebApi.Models.Equipment;
using WebApi.Models.Requests;
using WebApi.Models.Responses;
using IEquipmentService = WebApi.Services.Interfaces.IEquipmentService;

namespace WebApi.Services.Implementations;

public class EquipmentService : IEquipmentService
{
    public Task<EquipmentsResponse> GetEquipmentsAsync(EquipmentsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentModel> GetEquipmentByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentModel> CreateEquipmentAsync(CreateEquipmentModel model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PatchEquipmentModel> GetPatchModel(int equipmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<EquipmentModel> PatchEquipmentAsync(int equipmentId, PatchEquipmentModel request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
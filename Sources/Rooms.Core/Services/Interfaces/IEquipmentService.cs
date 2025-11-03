using Rooms.Core.Dtos;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Interfaces;

public interface IEquipmentService
{
    Task<EquipmentDto> GetEquipmentById(int equipmentId, CancellationToken cancellationToken);
    Task<EquipmentsResponseDto> FilterEquipments(GetEquipmentsDto dto, CancellationToken cancellationToken);
    Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto, CancellationToken cancellationToken);
    Task<EquipmentDto> PatchEquipment(int equipmentId, PatchEquipmentDto dto, CancellationToken cancellationToken);
    Task<FileExportDto> ExportEquipmentRegistry(CancellationToken cancellationToken);
}
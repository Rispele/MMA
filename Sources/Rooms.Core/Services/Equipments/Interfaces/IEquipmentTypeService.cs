using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Responses;

namespace Rooms.Core.Services.Equipments.Interfaces;

public interface IEquipmentTypeService
{
    Task<EquipmentTypeDto> GetEquipmentTypeById(int equipmentTypeId, CancellationToken cancellationToken);
    Task<EquipmentTypesResponseDto> FilterEquipmentTypes(GetEquipmentTypesDto dto, CancellationToken cancellationToken);
    Task<EquipmentTypeDto> CreateEquipmentType(CreateEquipmentTypeDto dto, CancellationToken cancellationToken);
    Task<EquipmentTypeDto> PatchEquipmentType(int equipmentTypeId, PatchEquipmentTypeDto dto, CancellationToken cancellationToken);
}
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;

namespace Rooms.Core.Interfaces.Services.Equipments;

public interface IEquipmentTypeService
{
    Task<EquipmentTypeDto> GetEquipmentTypeById(int equipmentTypeId, CancellationToken cancellationToken);
    Task<EquipmentTypesResponseDto> FilterEquipmentTypes(GetEquipmentTypesDto dto, CancellationToken cancellationToken);
    Task<EquipmentTypeDto> CreateEquipmentType(CreateEquipmentTypeDto dto, CancellationToken cancellationToken);
    Task<EquipmentTypeDto> PatchEquipmentType(int equipmentTypeId, PatchEquipmentTypeDto dto, CancellationToken cancellationToken);
}
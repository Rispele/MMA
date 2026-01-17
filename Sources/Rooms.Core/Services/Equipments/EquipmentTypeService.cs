using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Core.Services.Equipments.Mappers;
using Rooms.Domain.Models.Equipments;
using Rooms.Domain.Propagated.Exceptions;

namespace Rooms.Core.Services.Equipments;

internal class EquipmentTypeService(
    [RoomsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IRoomService roomService) : IEquipmentTypeService
{
    public async Task<EquipmentTypeDto> GetEquipmentTypeById(int equipmentTypeId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = await GetEquipmentTypeByIdInner(equipmentTypeId, cancellationToken, context);

        return EquipmentTypeDtoMapper.MapEquipmentTypeToDto(equipmentType);
    }

    public async Task<EquipmentTypesResponseDto> FilterEquipmentTypes(GetEquipmentTypesDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterEquipmentTypesQuery(dto.BatchSize, dto.BatchNumber, dto.Filter);

        var (equipmentTypesEnumerable, totalCount) = await context.ApplyQuery(query, cancellationToken);
        var equipmentTypes = await equipmentTypesEnumerable.ToListAsync(cancellationToken);

        var convertedEquipmentTypes = equipmentTypes.Select(EquipmentTypeDtoMapper.MapEquipmentTypeToDto).ToArray();

        return new EquipmentTypesResponseDto(convertedEquipmentTypes, totalCount);
    }

    public async Task<EquipmentTypeDto> CreateEquipmentType(CreateEquipmentTypeDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = new EquipmentType(
            dto.Name,
            dto.Parameters.Select(descriptor => new EquipmentParameterDescriptor(descriptor.Name, descriptor.Required)).ToList());

        context.Add(equipmentType);

        await context.Commit(cancellationToken);

        return EquipmentTypeDtoMapper.MapEquipmentTypeToDto(equipmentType);
    }

    public async Task<EquipmentTypeDto> PatchEquipmentType(
        int equipmentTypeId,
        PatchEquipmentTypeDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentTypeToPatch = await GetEquipmentTypeByIdInner(equipmentTypeId, cancellationToken, context);

        var updatedParameters = dto.Parameters
            .Select(descriptor => new EquipmentParameterDescriptor(descriptor.Name, descriptor.Required))
            .ToList();

        equipmentTypeToPatch.Update(dto.Name, updatedParameters);

        await context.Commit(cancellationToken);

        return EquipmentTypeDtoMapper.MapEquipmentTypeToDto(equipmentTypeToPatch);
    }

    private async Task<EquipmentType> GetEquipmentTypeByIdInner(
        int equipmentTypeId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindEquipmentTypeByIdQuery(equipmentTypeId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentTypeNotFoundException($"EquipmentType [{equipmentTypeId}] not found");
    }
}
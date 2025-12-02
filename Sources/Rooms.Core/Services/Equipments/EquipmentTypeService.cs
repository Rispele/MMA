using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Rooms.Core.Exceptions;
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

        var query = new FilterEquipmentTypesQuery(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentTypeId, dto.Filter);

        var equipmentTypes = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedEquipmentTypes = equipmentTypes.Select(EquipmentTypeDtoMapper.MapEquipmentTypeToDto).ToArray();
        int? lastEquipmentTypeId = convertedEquipmentTypes.Length == 0 ? null : convertedEquipmentTypes.Select(t => t.Id).Max();

        return new EquipmentTypesResponseDto(convertedEquipmentTypes, convertedEquipmentTypes.Length, lastEquipmentTypeId);
    }

    public async Task<EquipmentTypeDto> CreateEquipmentType(CreateEquipmentTypeDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        if (!dto.Parameters.Any())
        {
            throw new InvalidRequestException("Equipment type must have at least one parameter");
        }

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

        if (!dto.Parameters.Any())
        {
            throw new InvalidRequestException("Equipment type must have at least one parameter");
        }

        var newRequiredParameters = dto.Parameters
            .Where(x => x.Required
                        && (equipmentTypeToPatch.Parameters.All(y => x.Name != y.Name)
                            || !equipmentTypeToPatch.Parameters.First(y => x.Name == y.Name).Required))
            .ToArray();
        if (newRequiredParameters.Length != 0)
        {
            throw new InvalidRequestException("Equipment type should not strictly require new or already present parameters");
        }

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
using Commons;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Dtos.Requests.EquipmentTypes;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.Equipment;
using EquipmentTypeDtoConverter = Rooms.Core.DtoConverters.EquipmentTypeDtoConverter;

namespace Rooms.Core.Services.Implementations;

public class EquipmentTypeService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IEquipmentTypeQueryFactory equipmentTypeQueryFactory,
    IRoomService roomService) : IEquipmentTypeService
{
    public async Task<EquipmentTypeDto> GetEquipmentTypeById(int equipmentTypeId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = await GetEquipmentTypeByIdInner(equipmentTypeId, cancellationToken, context);

        return equipmentType.Map(EquipmentTypeDtoConverter.Convert);
    }

    public async Task<EquipmentTypesResponseDto> FilterEquipmentTypes(GetEquipmentTypesDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = equipmentTypeQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterEquipmentTypeId, dto.Filter);

        var equipmentTypes = await context
            .ApplyQuery(query)
            .ToArrayAsync(cancellationToken);

        var convertedEquipmentTypes = equipmentTypes.Select(EquipmentTypeDtoConverter.Convert).ToArray();
        int? lastEquipmentTypeId = convertedEquipmentTypes.Length == 0 ? null : convertedEquipmentTypes.Select(t => t.Id).Max();

        return new EquipmentTypesResponseDto(convertedEquipmentTypes, convertedEquipmentTypes.Length, lastEquipmentTypeId);
    }

    public async Task<EquipmentTypeDto> CreateEquipmentType(CreateEquipmentTypeDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentType = EquipmentType.New(
            dto.Name,
            dto.Parameters.Select(x => x.Map(EquipmentTypeDtoConverter.Convert)),
            []);;

        context.Add(equipmentType);

        await context.Commit(cancellationToken);

        return EquipmentTypeDtoConverter.Convert(equipmentType);
    }

    public async Task<EquipmentTypeDto> PatchEquipmentType(
        int equipmentTypeId,
        PatchEquipmentTypeDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var equipmentTypeToPatch = await GetEquipmentTypeByIdInner(equipmentTypeId, cancellationToken, context);

        equipmentTypeToPatch.Update(
            dto.Name,
            dto.Parameters.Select(x => x.Map(EquipmentTypeDtoConverter.Convert)),
            []);

        await context.Commit(cancellationToken);

        return EquipmentTypeDtoConverter.Convert(equipmentTypeToPatch);
    }

    private async Task<EquipmentType> GetEquipmentTypeByIdInner(
        int equipmentTypeId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = equipmentTypeQueryFactory.FindById(equipmentTypeId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new EquipmentTypeNotFoundException($"EquipmentType [{equipmentTypeId}] not found");
    }
}
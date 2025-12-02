using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Commons.ExternalClients.LkUsers;
using Rooms.Core.Exceptions;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Responses;
using Rooms.Core.Interfaces.Services.OperatorDepartments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Core.Services.OperatorDepartments.Mappers;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Propagated.Exceptions;

namespace Rooms.Core.Services.OperatorDepartments;

internal class OperatorDepartmentService(
    [RoomsScope] IUnitOfWorkFactory unitOfWorkFactory,
    IRoomService roomService,
    ILkUsersClient lkUsersClient) : IOperatorDepartmentService
{
    public async Task<OperatorDepartmentDto> GetOperatorDepartmentById(int operatorDepartmentId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var operatorDepartment = await GetOperatorDepartmentByIdInner(operatorDepartmentId, cancellationToken, context);

        return operatorDepartment.Map(OperatorDepartmentsDtoMapper.Map);
    }

    public async Task<OperatorDepartmentDto[]> GetOperatorDepartmentsById(int[] operatorDepartmentIds, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var request = new FindOperatorDepartmentByIdsQuery(operatorDepartmentIds);
        var response = await context.ApplyQuery(request, cancellationToken);

        return response.ToBlockingEnumerable(cancellationToken).Select(OperatorDepartmentsDtoMapper.Map).ToArray();
    }

    public async Task<Dictionary<Guid, string>> GetAvailableOperators(CancellationToken cancellationToken)
    {
        // todo
        return [];
    }

    public async Task<OperatorDepartmentsResponseDto> FilterOperatorDepartments(GetOperatorDepartmentsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterOperatorDepartmentsQuery(dto.BatchSize, dto.BatchNumber, dto.AfterOperatorDepartmentId, dto.Filter);

        var operatorDepartments = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedOperatorDepartments = operatorDepartments.Select(OperatorDepartmentsDtoMapper.Map).ToArray();
        int? lastOperatorDepartmentId = convertedOperatorDepartments.Length == 0 ? null : convertedOperatorDepartments.Select(t => t.Id).Max();

        return new OperatorDepartmentsResponseDto(convertedOperatorDepartments, convertedOperatorDepartments.Length, lastOperatorDepartmentId);
    }

    public async Task<OperatorDepartmentDto> CreateOperatorDepartment(CreateOperatorDepartmentDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var rooms = await roomService.FindRoomByIds(dto.RoomIds, cancellationToken);
        var missingRooms = dto.RoomIds.Where(x => rooms.All(y => y.Id != x)).ToArray();
        if (rooms.Length != dto.RoomIds.Distinct().Count())
        {
            throw new InvalidRequestException($"Rooms not found: [{string.Join(", ", missingRooms)}]");
        }

        if (dto.Operators.Keys.Count == 0)
        {
            throw new InvalidRequestException("Operator department must have at least one operator");
        }

        var operatorDepartment = new OperatorDepartment(dto.Name, dto.Contacts, dto.Operators);

        var enumerable = await context.ApplyQuery(new FindRoomsByIdQuery(dto.RoomIds), cancellationToken);
        await foreach (var room in enumerable.WithCancellation(cancellationToken))
        {
            operatorDepartment.AddRoom(room);
        }

        context.Add(operatorDepartment);

        await context.Commit(cancellationToken);

        return OperatorDepartmentsDtoMapper.Map(operatorDepartment);
    }

    public async Task<OperatorDepartmentDto> PatchOperatorDepartment(
        int operatorDepartmentId,
        PatchOperatorDepartmentDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var operatorDepartmentToPatch = await GetOperatorDepartmentByIdInner(operatorDepartmentId, cancellationToken, context);
        var roomsToRemove = operatorDepartmentToPatch.Rooms.Where(r => !dto.RoomIds.Contains(r.Id)).ToList();
        var roomIdsToAdd = dto.RoomIds.Where(roomId => operatorDepartmentToPatch.Rooms.All(r => r.Id != roomId)).ToList();

        var rooms = await roomService.FindRoomByIds(dto.RoomIds, cancellationToken);
        var missingRooms = dto.RoomIds.Where(x => rooms.All(y => y.Id != x)).ToArray();
        if (rooms.Length != dto.RoomIds.Distinct().Count())
        {
            throw new InvalidRequestException($"Rooms not found: [{string.Join(", ", missingRooms)}]");
        }

        if (dto.Operators.Keys.Count == 0)
        {
            throw new InvalidRequestException("Operator department must have at least one operator");
        }

        operatorDepartmentToPatch.Update(
            dto.Name,
            dto.Operators,
            dto.Contacts);

        roomsToRemove.ForEach(room => operatorDepartmentToPatch.RemoveRoom(room.Id));
        var enumerable = await context.ApplyQuery(new FindRoomsByIdQuery(dto.RoomIds), cancellationToken);
        await foreach (var room in enumerable.WithCancellation(cancellationToken))
        {
            operatorDepartmentToPatch.AddRoom(room);
        }

        await context.Commit(cancellationToken);

        return OperatorDepartmentsDtoMapper.Map(operatorDepartmentToPatch);
    }

    private async Task<OperatorDepartment> GetOperatorDepartmentByIdInner(
        int operatorDepartmentId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindOperatorDepartmentByIdQuery(operatorDepartmentId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new OperatorDepartmentNotFoundException($"Operator department [{operatorDepartmentId}] not found");
    }
}
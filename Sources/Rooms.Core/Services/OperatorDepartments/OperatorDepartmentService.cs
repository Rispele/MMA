using Commons;
using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.Requests.OperatorDepartments;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Core.Services.OperatorDepartments.Interfaces;
using Rooms.Core.Services.OperatorDepartments.Mappers;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Services.OperatorDepartments;

public class OperatorDepartmentService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IOperatorDepartmentClient operatorDepartmentClient) : IOperatorDepartmentService
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
        var operators = await operatorDepartmentClient.GetAvailableOperators();

        return operators;
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
using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Commons.ExternalClients.LkUsers;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Responses;
using Rooms.Core.Interfaces.Services.OperatorDepartments;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Core.Queries.Implementations.Room;
using Rooms.Core.Services.OperatorDepartments.Mappers;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Domain.Propagated.Exceptions;

namespace Rooms.Core.Services.OperatorDepartments;

internal class OperatorDepartmentService(
    [RoomsScope] IUnitOfWorkFactory unitOfWorkFactory,
    ILkUsersClient lkUsersClient) : IOperatorDepartmentService
{
    public async Task<OperatorDepartmentDto> GetOperatorDepartmentById(int operatorDepartmentId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var operatorDepartment = await GetOperatorDepartmentByIdInner(operatorDepartmentId, cancellationToken, context);

        return operatorDepartment.Map(OperatorDepartmentsDtoMapper.MapOperatorDepartmentToDto);
    }

    public async Task<OperatorDepartmentDto[]> GetOperatorDepartmentsById(int[] operatorDepartmentIds, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var request = new FindOperatorDepartmentByIdsQuery(operatorDepartmentIds);
        var response = await context.ApplyQuery(request, cancellationToken);

        return response.ToBlockingEnumerable(cancellationToken).Select(OperatorDepartmentsDtoMapper.MapOperatorDepartmentToDto).ToArray();
    }

    public async Task<OperatorDto[]> GetAvailableOperators(CancellationToken cancellationToken)
    {
        var operatorEmployees = await lkUsersClient.GetEmployees(cancellationToken);
        var operatorEmployeeUserIds = operatorEmployees.Select(x => x.UserId);
        var operatorUsers = await lkUsersClient.GetUsers(operatorEmployeeUserIds, cancellationToken);
        var operatorUsersById = operatorUsers.ToDictionary(x => x.UserId);

        return operatorEmployees.Select(x => new OperatorDto
        {
            Email = operatorUsersById[x.UserId].Email,
            FullName = x.FullName,
            UserId = x.UserId
        }).ToArray();
    }

    public async Task<OperatorDepartmentsResponseDto> FilterOperatorDepartments(GetOperatorDepartmentsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterOperatorDepartmentsQuery(dto.BatchSize, dto.BatchNumber, dto.Filter);

        var (operatorDepartmentsEnumerable, totalCount) = await context.ApplyQuery(query, cancellationToken);
        var operatorDepartments = await operatorDepartmentsEnumerable.ToListAsync(cancellationToken);

        var convertedOperatorDepartments = operatorDepartments.Select(OperatorDepartmentsDtoMapper.MapOperatorDepartmentToDto).ToArray();

        return new OperatorDepartmentsResponseDto(convertedOperatorDepartments, totalCount);
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

        return OperatorDepartmentsDtoMapper.MapOperatorDepartmentToDto(operatorDepartment);
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

        return OperatorDepartmentsDtoMapper.MapOperatorDepartmentToDto(operatorDepartmentToPatch);
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
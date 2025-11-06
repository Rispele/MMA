using Commons;
using Rooms.Core.Clients;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.Requests.OperatorDepartments;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Core.Services.Implementations;

public class OperatorDepartmentService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IOperatorDepartmentQueryFactory operatorDepartmentQueryFactory,
    IRoomQueriesFactory roomQueriesFactory,
    IOperatorDepartmentClient operatorDepartmentClient) : IOperatorDepartmentService
{
    public async Task<OperatorDepartmentDto> GetOperatorDepartmentById(int operatorDepartmentId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var operatorDepartment = await GetOperatorDepartmentByIdInner(operatorDepartmentId, cancellationToken, context);

        return operatorDepartment.Map(OperatorDepartmentsDtoConverter.Convert);
    }

    public async Task<Dictionary<Guid, string>> GetAvailableOperators(CancellationToken cancellationToken)
    {
        var operators = await operatorDepartmentClient.GetAvailableOperators();

        return operators;
    }

    public async Task<OperatorDepartmentsResponseDto> FilterOperatorDepartments(GetOperatorDepartmentsDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = operatorDepartmentQueryFactory.Filter(dto.BatchSize, dto.BatchNumber, dto.AfterOperatorDepartmentId, dto.Filter);

        var operatorDepartments = await context
            .ApplyQuery(query)
            .ToArrayAsync(cancellationToken);

        var convertedOperatorDepartments = operatorDepartments.Select(OperatorDepartmentsDtoConverter.Convert).ToArray();
        int? lastOperatorDepartmentId = convertedOperatorDepartments.Length == 0 ? null : convertedOperatorDepartments.Select(t => t.Id).Max();

        return new OperatorDepartmentsResponseDto(convertedOperatorDepartments, convertedOperatorDepartments.Length, lastOperatorDepartmentId);
    }

    public async Task<OperatorDepartmentDto> CreateOperatorDepartment(CreateOperatorDepartmentDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);


        var operatorDepartment = new OperatorDepartment
        {
            Name = dto.Name,
            Operators = dto.Operators,
            Contacts = dto.Contacts
        };

        await foreach (var room in context.ApplyQuery(roomQueriesFactory.FindByIds(dto.RoomIds)).WithCancellation(cancellationToken))
        {
            operatorDepartment.AddRoom(room);
        }

        context.Add(operatorDepartment);

        await context.Commit(cancellationToken);

        return OperatorDepartmentsDtoConverter.Convert(operatorDepartment);
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
        await foreach (var room in context.ApplyQuery(roomQueriesFactory.FindByIds(roomIdsToAdd)).WithCancellation(cancellationToken))
        {
            operatorDepartmentToPatch.AddRoom(room);
        }

        await context.Commit(cancellationToken);

        return OperatorDepartmentsDtoConverter.Convert(operatorDepartmentToPatch);
    }

    private async Task<OperatorDepartment> GetOperatorDepartmentByIdInner(
        int operatorDepartmentId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = operatorDepartmentQueryFactory.FindById(operatorDepartmentId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new OperatorDepartmentNotFoundException($"Operator department [{operatorDepartmentId}] not found");
    }
}
using Commons;
using Rooms.Core.Clients.Interfaces;
using Rooms.Core.Clients.LkUsers;
using Rooms.Core.Dtos.InstituteCoordinator;
using Rooms.Core.Dtos.InstituteCoordinator.Requests;
using Rooms.Core.Dtos.InstituteCoordinator.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.InstituteResponsible;
using Rooms.Core.Services.InstituteCoordinators.Interfaces;
using Rooms.Core.Services.InstituteCoordinators.Mappers;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.InstituteCoordinators;

namespace Rooms.Core.Services.InstituteCoordinators;

public class InstituteResponsibleService(
    IUnitOfWorkFactory unitOfWorkFactory,
    ILkUsersClient lkUsersClient,
    IInstituteDepartmentClient instituteDepartmentClient) : IInstituteResponsibleService
{
    public async Task<InstituteCoordinatorDto> GetInstituteResponsibleById(int instituteResponsibleId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsible = await GetInstituteResponsibleByIdInner(instituteResponsibleId, cancellationToken, context);

        return InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto(instituteResponsible);
    }

    public async Task<LkEmployeeDto[]> GetAvailableInstituteResponsible(CancellationToken cancellationToken)
    {
        var responsible = await lkUsersClient.GetEmployees(cancellationToken);

        return responsible;
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteDepartments(CancellationToken cancellationToken)
    {
        var departments = await instituteDepartmentClient.GetAvailableInstituteDepartments();

        return departments;
    }

    public async Task<InstituteCoordinatorResponseDto> FilterInstituteResponsible(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterInstituteResponsibleQuery(dto.BatchSize, dto.BatchNumber, dto.AfterInstituteResponsibleId, dto.Filter);

        var instituteResponsible = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedInstituteResponsible = instituteResponsible.Select(InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto).ToArray();
        int? lastInstituteResponsibleId = convertedInstituteResponsible.Length == 0 ? null : convertedInstituteResponsible.Select(t => t.Id).Max();

        return new InstituteCoordinatorResponseDto(convertedInstituteResponsible, convertedInstituteResponsible.Length, lastInstituteResponsibleId);
    }

    public async Task<InstituteCoordinatorDto> CreateInstituteResponsible(CreateInstituteCoordinatorDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsible = new InstituteCoordinator(
            dto.Institute,
            dto.Coordinators.Select(t => new Coordinator(t.Id, t.FullName)).ToArray());

        context.Add(instituteResponsible);

        await context.Commit(cancellationToken);

        return InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto(instituteResponsible);
    }

    public async Task<InstituteCoordinatorDto> PatchInstituteResponsible(
        int instituteResponsibleId,
        PatchInstituteCoordinatorDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsibleToPatch = await GetInstituteResponsibleByIdInner(instituteResponsibleId, cancellationToken, context);

        instituteResponsibleToPatch.Update(
            dto.Institute,
            dto.Coordinators.Select(t => new Coordinator(t.Id, t.FullName)).ToArray());

        await context.Commit(cancellationToken);

        return InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto(instituteResponsibleToPatch);
    }

    private async Task<InstituteCoordinator> GetInstituteResponsibleByIdInner(
        int instituteResponsibleId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindInstituteResponsibleByIdQuery(instituteResponsibleId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new InstituteResponsibleNotFoundException($"InstituteResponsible [{instituteResponsibleId}] not found");
    }
}
using Booking.Core.Exceptions;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Responses;
using Booking.Core.Interfaces.Services.InstituteCoordinators;
using Booking.Core.Queries.InstituteCoordinators;
using Booking.Core.Services.InstituteCoordinators.Mappers;
using Booking.Domain.Models.InstituteCoordinators;
using Commons;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Commons.ExternalClients.InstituteDepartments;
using Commons.ExternalClients.LkUsers;

namespace Booking.Core.Services.InstituteCoordinators;

public class InstituteCoordinatorsService(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    ILkUsersClient lkUsersClient,
    IInstituteDepartmentClient instituteDepartmentClient) : IInstituteCoordinatorsService
{
    public async Task<InstituteCoordinatorDto> GetInstituteResponsibleById(int instituteResponsibleId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsible = await GetInstituteResponsibleByIdInner(instituteResponsibleId, cancellationToken, context);

        return InstituteCoordinatorsDtoMapper.MapInstituteResponsibleToDto(instituteResponsible);
    }

    public async Task<LkEmployeeDto[]> GetAvailableInstituteResponsible(CancellationToken cancellationToken)
    {
        var responsible = await lkUsersClient.GetEmployees(cancellationToken);

        return responsible;
    }

    public async Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartments(CancellationToken cancellationToken)
    {
        var departments = await instituteDepartmentClient.GetAvailableInstituteDepartments();

        return departments;
    }

    public async Task<InstituteCoordinatorResponseDto> FilterInstituteResponsible(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterInstituteCoordinatorsQuery(dto.BatchSize, dto.BatchNumber, dto.AfterId, dto.Filter);

        var instituteResponsible = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedInstituteResponsible = instituteResponsible.Select(InstituteCoordinatorsDtoMapper.MapInstituteResponsibleToDto).ToArray();
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

        return InstituteCoordinatorsDtoMapper.MapInstituteResponsibleToDto(instituteResponsible);
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

        return InstituteCoordinatorsDtoMapper.MapInstituteResponsibleToDto(instituteResponsibleToPatch);
    }

    private async Task<InstituteCoordinator> GetInstituteResponsibleByIdInner(
        int instituteResponsibleId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindInstituteCoordinatorByIdQuery(instituteResponsibleId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new InstituteCoordinatorNotFoundException($"InstituteResponsible [{instituteResponsibleId}] not found");
    }
}
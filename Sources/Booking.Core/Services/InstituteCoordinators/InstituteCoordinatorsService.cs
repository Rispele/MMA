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

public class InstituteCoordinatorService(
    [BookingsScope] IUnitOfWorkFactory unitOfWorkFactory,
    ILkUsersClient lkUsersClient,
    IInstituteDepartmentClient instituteDepartmentClient) : IInstituteCoordinatorService
{
    public async Task<InstituteCoordinatorDto> GetInstituteCoordinatorById(int instituteCoordinatorId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteCoordinator = await GetInstituteCoordinatorByIdInner(instituteCoordinatorId, cancellationToken, context);
        var dto = InstituteCoordinatorsDtoMapper.MapInstituteCoordinatorToDto(instituteCoordinator);

        var institutes = await instituteDepartmentClient.GetAvailableInstituteDepartments();
        var matchedInstitute = institutes.FirstOrDefault(x => x.Id == dto.InstituteId.ToString());
        dto.InstituteName = matchedInstitute!.InstituteName;

        return dto;
    }

    public async Task<InstituteCoordinatorEmployeeDto[]> GetAvailableInstituteCoordinators(CancellationToken cancellationToken)
    {
        var coordinators = await lkUsersClient.GetEmployees(cancellationToken);

        return coordinators.Select(InstituteCoordinatorsDtoMapper.MapInstituteCoordinatorEmployeeToDto).ToArray();
    }

    public async Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartments(CancellationToken cancellationToken)
    {
        var departments = await instituteDepartmentClient.GetAvailableInstituteDepartments();

        return departments;
    }

    public async Task<InstituteCoordinatorsResponseDto> FilterInstituteCoordinators(GetInstituteCoordinatorDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterInstituteCoordinatorsQuery(dto.BatchSize, dto.BatchNumber, dto.Filter);

        var (instituteCoordinatorsEnumerable, totalCount) = await context.ApplyQuery(query, cancellationToken);
        var instituteCoordinators = await instituteCoordinatorsEnumerable.ToListAsync(cancellationToken);

        var convertedInstituteCoordinators = instituteCoordinators.Select(InstituteCoordinatorsDtoMapper.MapInstituteCoordinatorToDto).ToArray();

        var institutesById = (await instituteDepartmentClient.GetAvailableInstituteDepartments()).ToDictionary(x => x.Id);
        foreach (var coordinator in convertedInstituteCoordinators)
        {
            coordinator.InstituteName = institutesById.TryGetValue(coordinator.InstituteId.ToString(), out var institute)
                ? institute.InstituteName : throw new InstituteNotFoundException($"Institite [{coordinator.InstituteId}] not found");
        }

        return new InstituteCoordinatorsResponseDto(convertedInstituteCoordinators, totalCount);
    }

    public async Task<InstituteCoordinatorDto> CreateInstituteCoordinator(CreateInstituteCoordinatorDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteCoordinator = new InstituteCoordinator(
            dto.InstituteId,
            dto.Coordinators.Select(t => new Coordinator(t.Id, t.FullName)).ToArray());

        context.Add(instituteCoordinator);

        await context.Commit(cancellationToken);

        var resultDto = InstituteCoordinatorsDtoMapper.MapInstituteCoordinatorToDto(instituteCoordinator);

        var institutes = await instituteDepartmentClient.GetAvailableInstituteDepartments();
        var matchedInstitute = institutes.FirstOrDefault(x => x.Id == resultDto.InstituteId.ToString());
        resultDto.InstituteName = matchedInstitute!.InstituteName;

        return resultDto;
    }

    public async Task<InstituteCoordinatorDto> PatchInstituteCoordinator(
        int instituteCoordinatorId,
        PatchInstituteCoordinatorDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteCoordinatorToPatch = await GetInstituteCoordinatorByIdInner(instituteCoordinatorId, cancellationToken, context);

        instituteCoordinatorToPatch.Update(
            dto.InstituteId,
            dto.Coordinators.Select(t => new Coordinator(t.Id, t.FullName)).ToArray());

        await context.Commit(cancellationToken);

        var resultDto = InstituteCoordinatorsDtoMapper.MapInstituteCoordinatorToDto(instituteCoordinatorToPatch);

        var institutes = await instituteDepartmentClient.GetAvailableInstituteDepartments();
        var matchedInstitute = institutes.FirstOrDefault(x => x.Id == resultDto.InstituteId.ToString());
        resultDto.InstituteName = matchedInstitute!.InstituteName;

        return resultDto;
    }

    private async Task<InstituteCoordinator> GetInstituteCoordinatorByIdInner(
        int instituteCoordinatorId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindInstituteCoordinatorByIdQuery(instituteCoordinatorId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new InstituteCoordinatorNotFoundException($"InstituteCoordinator [{instituteCoordinatorId}] not found");
    }
}
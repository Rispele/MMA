using Commons;
using Rooms.Core.Clients.Interfaces;
using Rooms.Core.DtoConverters;
using Rooms.Core.Dtos.InstituteResponsible;
using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Queries.Abstractions;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.InstituteResponsible;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using Rooms.Domain.Models.InstituteResponsible;

namespace Rooms.Core.Services.Implementations;

public class InstituteResponsibleService(
    IUnitOfWorkFactory unitOfWorkFactory,
    IInstituteResponsibleClient instituteResponsibleClient,
    IInstituteDepartmentClient instituteDepartmentClient) : IInstituteResponsibleService
{
    public async Task<InstituteResponsibleDto> GetInstituteResponsibleById(int instituteResponsibleId, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsible = await GetInstituteResponsibleByIdInner(instituteResponsibleId, cancellationToken, context);

        return InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto(instituteResponsible);
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteResponsible(CancellationToken cancellationToken)
    {
        var responsible = await instituteResponsibleClient.GetAvailableInstituteResponsible();

        return responsible;
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteDepartments(CancellationToken cancellationToken)
    {
        var departments = await instituteDepartmentClient.GetAvailableInstituteDepartments();

        return departments;
    }

    public async Task<InstituteResponsibleResponseDto> FilterInstituteResponsible(GetInstituteResponsibleDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var query = new FilterInstituteResponsibleQuery(dto.BatchSize, dto.BatchNumber, dto.AfterInstituteResponsibleId, dto.Filter);

        var instituteResponsible = await (await context.ApplyQuery(query, cancellationToken)).ToListAsync(cancellationToken);

        var convertedInstituteResponsible = instituteResponsible.Select(InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto).ToArray();
        int? lastInstituteResponsibleId = convertedInstituteResponsible.Length == 0 ? null : convertedInstituteResponsible.Select(t => t.Id).Max();

        return new InstituteResponsibleResponseDto(convertedInstituteResponsible, convertedInstituteResponsible.Length, lastInstituteResponsibleId);
    }

    public async Task<InstituteResponsibleDto> CreateInstituteResponsible(CreateInstituteResponsibleDto dto, CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsible = new InstituteResponsible
        {
            Institute = dto.Institute,
            Responsible = dto.Responsible
        };

        context.Add(instituteResponsible);

        await context.Commit(cancellationToken);

        return InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto(instituteResponsible);
    }

    public async Task<InstituteResponsibleDto> PatchInstituteResponsible(
        int instituteResponsibleId,
        PatchInstituteResponsibleDto dto,
        CancellationToken cancellationToken)
    {
        await using var context = await unitOfWorkFactory.Create(cancellationToken);

        var instituteResponsibleToPatch = await GetInstituteResponsibleByIdInner(instituteResponsibleId, cancellationToken, context);

        instituteResponsibleToPatch.Update(
            dto.Institute,
            dto.Responsible);

        await context.Commit(cancellationToken);

        return InstituteResponsibleDtoMapper.MapInstituteResponsibleToDto(instituteResponsibleToPatch);
    }

    private async Task<InstituteResponsible> GetInstituteResponsibleByIdInner(
        int instituteResponsibleId,
        CancellationToken cancellationToken,
        IUnitOfWork context)
    {
        var query = new FindInstituteResponsibleByIdQuery(instituteResponsibleId);

        return await context.ApplyQuery(query, cancellationToken) ??
               throw new InstituteResponsibleNotFoundException($"InstituteResponsible [{instituteResponsibleId}] not found");
    }
}
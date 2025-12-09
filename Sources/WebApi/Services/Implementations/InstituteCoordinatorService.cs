using Booking.Core.Interfaces.Services.InstituteCoordinators;
using Commons.ExternalClients.LkUsers;
using WebApi.ModelConverters;
using WebApi.Models.InstituteCoordinator;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class InstituteCoordinatorService(IInstituteCoordinatorsService instituteCoordinatorService) : IInstituteResponsibleService
{
    public async Task<InstituteResponsibleResponseModel> GetInstituteResponsibleAsync(
        GetInstituteCoordinatorModel model,
        CancellationToken cancellationToken)
    {
        var getInstituteResponsibleRequest = InstituteCoordinatorModelMapper.MapGetInstituteCoordinatorFromModel(model);

        var batch = await instituteCoordinatorService.FilterInstituteResponsible(getInstituteResponsibleRequest, cancellationToken);

        return new InstituteResponsibleResponseModel
        {
            InstituteResponsible = batch.InstituteResponsible.Select(InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<InstituteCoordinatorModel> GetInstituteResponsibleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteCoordinatorService.GetInstituteResponsibleById(id, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel(instituteResponsible);
    }

    public async Task<LkEmployeeDto[]> GetAvailableInstituteResponsibleAsync(CancellationToken cancellationToken)
    {
        var responsible = await instituteCoordinatorService.GetAvailableInstituteResponsible(cancellationToken);

        return responsible;
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken)
    {
        var departments = await instituteCoordinatorService.GetAvailableInstituteDepartments(cancellationToken);

        return departments;
    }

    public async Task<InstituteCoordinatorModel> CreateInstituteResponsibleAsync(
        CreateInstituteCoordinatorModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = InstituteCoordinatorModelMapper.MapCreateInstituteCoordinatorFromModel(model);

        var instituteResponsible = await instituteCoordinatorService.CreateInstituteResponsible(innerRequest, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel(instituteResponsible);
    }

    public async Task<PatchInstituteCoordinatorModel> GetInstituteResponsiblePatchModel(
        int instituteResponsibleId,
        CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteCoordinatorService.GetInstituteResponsibleById(instituteResponsibleId, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToPatchModel(instituteResponsible);
    }

    public async Task<InstituteCoordinatorModel> PatchInstituteResponsibleAsync(
        int instituteResponsibleId,
        PatchInstituteCoordinatorModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = InstituteCoordinatorModelMapper.MapPatchInstituteCoordinatorTypeFromModel(patchModel);

        var patched = await instituteCoordinatorService.PatchInstituteResponsible(instituteResponsibleId, patchRequest, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel(patched);
    }
}
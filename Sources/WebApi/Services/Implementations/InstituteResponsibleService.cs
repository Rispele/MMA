using Booking.Core.Services.InstituteCoordinators.Interfaces;
using WebApi.ModelConverters;
using WebApi.Models.InstituteCoordinator;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class InstituteResponsibleService(IInstituteCoordinatorsService instituteCoordinatorService) : IInstituteResponsibleService
{
    public async Task<InstituteResponsibleResponseModel> GetInstituteResponsibleAsync(
        GetInstituteResponsibleModel model,
        CancellationToken cancellationToken)
    {
        var getInstituteResponsibleRequest = InstituteResponsibleModelMapper.MapGetInstituteResponsibleFromModel(model);

        var batch = await instituteCoordinatorService.FilterInstituteResponsible(getInstituteResponsibleRequest, cancellationToken);

        return new InstituteResponsibleResponseModel
        {
            InstituteResponsible = batch.InstituteResponsible.Select(InstituteResponsibleModelMapper.MapInstituteResponsibleToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<InstituteCoordinatorModel> GetInstituteResponsibleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteCoordinatorService.GetInstituteResponsibleById(id, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToModel(instituteResponsible);
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteResponsibleAsync(CancellationToken cancellationToken)
    {
        var responsible = await instituteCoordinatorService.GetAvailableInstituteDepartments(cancellationToken);

        return responsible;
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken)
    {
        var departments = await instituteCoordinatorService.GetAvailableInstituteDepartments(cancellationToken);

        return departments;
    }

    public async Task<InstituteCoordinatorModel> CreateInstituteResponsibleAsync(
        CreateInstituteResponsibleModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = InstituteResponsibleModelMapper.MapCreateInstituteResponsibleFromModel(model);

        var instituteResponsible = await instituteCoordinatorService.CreateInstituteResponsible(innerRequest, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToModel(instituteResponsible);
    }

    public async Task<PatchInstituteResponsibleModel> GetInstituteResponsiblePatchModel(
        int instituteResponsibleId,
        CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteCoordinatorService.GetInstituteResponsibleById(instituteResponsibleId, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToPatchModel(instituteResponsible);
    }

    public async Task<InstituteCoordinatorModel> PatchInstituteResponsibleAsync(
        int instituteResponsibleId,
        PatchInstituteResponsibleModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = InstituteResponsibleModelMapper.MapPatchInstituteResponsibleTypeFromModel(patchModel);

        var patched = await instituteCoordinatorService.PatchInstituteResponsible(instituteResponsibleId, patchRequest, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToModel(patched);
    }
}
using WebApi.ModelConverters;
using WebApi.Models.InstituteResponsible;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations;

public class InstituteResponsibleService(Rooms.Core.Services.InstituteResponsibles.Interfaces.IInstituteResponsibleService instituteResponsibleService) : IInstituteResponsibleService
{
    public async Task<InstituteResponsibleResponseModel> GetInstituteResponsibleAsync(
        GetInstituteResponsibleModel model,
        CancellationToken cancellationToken)
    {
        var getInstituteResponsibleRequest = InstituteResponsibleModelMapper.MapGetInstituteResponsibleFromModel(model);

        var batch = await instituteResponsibleService.FilterInstituteResponsible(getInstituteResponsibleRequest, cancellationToken);

        return new InstituteResponsibleResponseModel
        {
            InstituteResponsible = batch.InstituteResponsible.Select(InstituteResponsibleModelMapper.MapInstituteResponsibleToModel).ToArray(),
            Count = batch.Count
        };
    }

    public async Task<InstituteResponsibleModel> GetInstituteResponsibleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteResponsibleService.GetInstituteResponsibleById(id, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToModel(instituteResponsible);
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteResponsibleAsync(CancellationToken cancellationToken)
    {
        var responsible = await instituteResponsibleService.GetAvailableInstituteDepartments(cancellationToken);

        return responsible;
    }

    public async Task<Dictionary<string, string>> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken)
    {
        var departments = await instituteResponsibleService.GetAvailableInstituteDepartments(cancellationToken);

        return departments;
    }

    public async Task<InstituteResponsibleModel> CreateInstituteResponsibleAsync(
        CreateInstituteResponsibleModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = InstituteResponsibleModelMapper.MapCreateInstituteResponsibleFromModel(model);

        var instituteResponsible = await instituteResponsibleService.CreateInstituteResponsible(innerRequest, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToModel(instituteResponsible);
    }

    public async Task<PatchInstituteResponsibleModel> GetInstituteResponsiblePatchModel(
        int instituteResponsibleId,
        CancellationToken cancellationToken)
    {
        var instituteResponsible = await instituteResponsibleService.GetInstituteResponsibleById(instituteResponsibleId, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToPatchModel(instituteResponsible);
    }

    public async Task<InstituteResponsibleModel> PatchInstituteResponsibleAsync(
        int instituteResponsibleId,
        PatchInstituteResponsibleModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = InstituteResponsibleModelMapper.MapPatchInstituteResponsibleTypeFromModel(patchModel);

        var patched = await instituteResponsibleService.PatchInstituteResponsible(instituteResponsibleId, patchRequest, cancellationToken);

        return InstituteResponsibleModelMapper.MapInstituteResponsibleToModel(patched);
    }
}
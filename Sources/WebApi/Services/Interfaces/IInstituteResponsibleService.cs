using WebApi.Models.InstituteResponsible;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IInstituteResponsibleService
{
    Task<InstituteResponsibleResponseModel> GetInstituteResponsibleAsync(GetInstituteResponsibleModel model, CancellationToken cancellationToken);
    Task<InstituteResponsibleModel> GetInstituteResponsibleByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteResponsibleAsync(CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken); // обернуть в модельку ответ
    Task<InstituteResponsibleModel> CreateInstituteResponsibleAsync(CreateInstituteResponsibleModel model, CancellationToken cancellationToken);
    Task<PatchInstituteResponsibleModel> GetInstituteResponsiblePatchModel(int instituteResponsibleId, CancellationToken cancellationToken);
    Task<InstituteResponsibleModel> PatchInstituteResponsibleAsync(
        int instituteResponsibleId,
        PatchInstituteResponsibleModel request,
        CancellationToken cancellationToken);
}
using WebApi.Models.InstituteCoordinator;
using WebApi.Models.Requests.InstituteResponsible;
using WebApi.Models.Responses;

namespace WebApi.Services.Interfaces;

public interface IInstituteResponsibleService
{
    Task<InstituteResponsibleResponseModel> GetInstituteResponsibleAsync(GetInstituteCoordinatorModel model, CancellationToken cancellationToken);
    Task<InstituteCoordinatorModel> GetInstituteResponsibleByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteResponsibleAsync(CancellationToken cancellationToken);
    Task<Dictionary<string, string>> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken); // обернуть в модельку ответ
    Task<InstituteCoordinatorModel> CreateInstituteResponsibleAsync(CreateInstituteCoordinatorModel model, CancellationToken cancellationToken);
    Task<PatchInstituteCoordinatorModel> GetInstituteResponsiblePatchModel(int instituteResponsibleId, CancellationToken cancellationToken);

    Task<InstituteCoordinatorModel> PatchInstituteResponsibleAsync(
        int instituteResponsibleId,
        PatchInstituteCoordinatorModel request,
        CancellationToken cancellationToken);
}
using Commons.ExternalClients.InstituteDepartments;
using Commons.ExternalClients.LkUsers;
using WebApi.Core.Models.InstituteCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.InstituteResponsible;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IInstituteResponsibleService
{
    Task<InstituteResponsibleResponseModel> GetInstituteResponsibleAsync(GetRequest<InstituteCoordinatorFilterModel> model, CancellationToken cancellationToken);
    Task<InstituteCoordinatorModel> GetInstituteResponsibleByIdAsync(int id, CancellationToken cancellationToken);
    Task<LkEmployeeDto[]> GetAvailableInstituteResponsibleAsync(CancellationToken cancellationToken);
    Task<InstituteDepartmentResponseDto[]> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken); // обернуть в модельку ответ
    Task<InstituteCoordinatorModel> CreateInstituteResponsibleAsync(CreateInstituteCoordinatorModel model, CancellationToken cancellationToken);
    Task<PatchInstituteCoordinatorModel> GetInstituteResponsiblePatchModel(int instituteResponsibleId, CancellationToken cancellationToken);

    Task<InstituteCoordinatorModel> PatchInstituteResponsibleAsync(
        int instituteResponsibleId,
        PatchInstituteCoordinatorModel request,
        CancellationToken cancellationToken);
}
using WebApi.Core.Models.InstituteCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.InstituteCoordinators;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Interfaces;

public interface IInstituteCoordinatorService
{
    Task<InstituteCoordinatorsResponseModel> GetInstituteCoordinatorsAsync(GetRequest<InstituteCoordinatorsFilterModel> model, CancellationToken cancellationToken);
    Task<InstituteCoordinatorModel> GetInstituteCoordinatorByIdAsync(int id, CancellationToken cancellationToken);
    Task<InstituteCoordinatorEmployeeModel[]> GetAvailableInstituteCoordinatorsAsync(CancellationToken cancellationToken);
    Task<InstituteDepartmentResponseModel[]> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken); // обернуть в модельку ответ
    Task<InstituteCoordinatorModel> CreateInstituteCoordinatorAsync(CreateInstituteCoordinatorModel model, CancellationToken cancellationToken);
    Task<PatchInstituteCoordinatorModel> GetInstituteCoordinatorPatchModel(int instituteCoordinatorId, CancellationToken cancellationToken);

    Task<InstituteCoordinatorModel> PatchInstituteCoordinatorAsync(
        int instituteCoordinatorId,
        PatchInstituteCoordinatorModel request,
        CancellationToken cancellationToken);
}
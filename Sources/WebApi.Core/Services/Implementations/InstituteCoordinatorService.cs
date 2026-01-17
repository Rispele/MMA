using Booking.Core.Interfaces.Services.InstituteCoordinators;
using WebApi.Core.ModelConverters;
using WebApi.Core.Models.InstituteCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.InstituteCoordinators;
using WebApi.Core.Models.Responses;

namespace WebApi.Core.Services.Implementations;

public class InstituteCoordinatorService(IInstituteCoordinatorService instituteCoordinatorService) : Interfaces.IInstituteCoordinatorService
{
    public async Task<InstituteCoordinatorsResponseModel> GetInstituteCoordinatorsAsync(
        GetRequest<InstituteCoordinatorsFilterModel>  model,
        CancellationToken cancellationToken)
    {
        var getInstituteCoordinatorsRequest = InstituteCoordinatorModelMapper.MapGetInstituteCoordinatorFromModel(model);

        var batch = await instituteCoordinatorService.FilterInstituteCoordinators(getInstituteCoordinatorsRequest, cancellationToken);

        return new InstituteCoordinatorsResponseModel(
            batch.InstituteCoordinators.Select(InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel).ToArray(),
            batch.TotalCount);
    }

    public async Task<InstituteCoordinatorModel> GetInstituteCoordinatorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var instituteCoordinator = await instituteCoordinatorService.GetInstituteCoordinatorById(id, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel(instituteCoordinator);
    }

    public async Task<InstituteCoordinatorEmployeeModel[]> GetAvailableInstituteCoordinatorsAsync(CancellationToken cancellationToken)
    {
        var coordinators = await instituteCoordinatorService.GetAvailableInstituteCoordinators(cancellationToken);

        return coordinators.Select(InstituteCoordinatorModelMapper.MapInstituteCoordinatorEmployeeToModel).ToArray();
    }

    public async Task<InstituteDepartmentResponseModel[]> GetAvailableInstituteDepartmentsAsync(CancellationToken cancellationToken)
    {
        var departments = await instituteCoordinatorService.GetAvailableInstituteDepartments(cancellationToken);

        return departments.Select(InstituteCoordinatorModelMapper.MapInstituteDepartmentResponseToModel).ToArray();
    }

    public async Task<InstituteCoordinatorModel> CreateInstituteCoordinatorAsync(
        CreateInstituteCoordinatorModel model,
        CancellationToken cancellationToken)
    {
        var innerRequest = InstituteCoordinatorModelMapper.MapCreateInstituteCoordinatorFromModel(model);

        var instituteCoordinator = await instituteCoordinatorService.CreateInstituteCoordinator(innerRequest, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel(instituteCoordinator);
    }

    public async Task<PatchInstituteCoordinatorModel> GetInstituteCoordinatorPatchModel(
        int instituteCoordinatorId,
        CancellationToken cancellationToken)
    {
        var instituteCoordinator = await instituteCoordinatorService.GetInstituteCoordinatorById(instituteCoordinatorId, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToPatchModel(instituteCoordinator);
    }

    public async Task<InstituteCoordinatorModel> PatchInstituteCoordinatorAsync(
        int instituteCoordinatorId,
        PatchInstituteCoordinatorModel patchModel,
        CancellationToken cancellationToken)
    {
        var patchRequest = InstituteCoordinatorModelMapper.MapPatchInstituteCoordinatorTypeFromModel(patchModel);

        var patched = await instituteCoordinatorService.PatchInstituteCoordinator(instituteCoordinatorId, patchRequest, cancellationToken);

        return InstituteCoordinatorModelMapper.MapInstituteCoordinatorToModel(patched);
    }
}
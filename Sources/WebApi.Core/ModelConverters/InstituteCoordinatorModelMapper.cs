using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Commons.ExternalClients.InstituteDepartments;
using Riok.Mapperly.Abstractions;
using WebApi.Core.Models.InstituteCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.InstituteCoordinators;

// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.Core.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteCoordinatorModelMapper
{
    public static partial InstituteCoordinatorModel MapInstituteCoordinatorToModel(InstituteCoordinatorDto instituteCoordinator);

    public static partial InstituteCoordinatorEmployeeModel MapInstituteCoordinatorEmployeeToModel(InstituteCoordinatorEmployeeDto instituteCoordinator);

    [MapperIgnoreSource(nameof(InstituteCoordinatorDto.Id))]
    [MapperIgnoreSource(nameof(InstituteCoordinatorDto.InstituteName))]
    public static partial PatchInstituteCoordinatorModel MapInstituteCoordinatorToPatchModel(InstituteCoordinatorDto instituteCoordinator);

    public static partial CreateInstituteCoordinatorDto MapCreateInstituteCoordinatorFromModel(CreateInstituteCoordinatorModel instituteCoordinator);

    public static partial PatchInstituteCoordinatorDto MapPatchInstituteCoordinatorTypeFromModel(PatchInstituteCoordinatorModel instituteCoordinator);

    [MapProperty(nameof(GetRequest<InstituteCoordinatorsFilterModel>.PageSize), nameof(GetInstituteCoordinatorDto.BatchSize))]
    [MapProperty(
        nameof(GetRequest<InstituteCoordinatorsFilterModel>.Page),
        nameof(GetInstituteCoordinatorDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetInstituteCoordinatorDto MapGetInstituteCoordinatorFromModel(GetRequest<InstituteCoordinatorsFilterModel> model);

    public static partial InstituteDepartmentResponseModel MapInstituteDepartmentResponseToModel(InstituteDepartmentResponseDto instituteDepartment);
}
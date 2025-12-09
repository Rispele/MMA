using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Riok.Mapperly.Abstractions;
using WebApi.Core.Models.InstituteCoordinator;
using WebApi.Core.Models.Requests;
using WebApi.Core.Models.Requests.InstituteResponsible;

// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.Core.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteCoordinatorModelMapper
{
    public static partial InstituteCoordinatorModel MapInstituteCoordinatorToModel(InstituteCoordinatorDto instituteCoordinator);

    [MapperIgnoreSource(nameof(InstituteCoordinatorDto.Id))]
    public static partial PatchInstituteCoordinatorModel MapInstituteCoordinatorToPatchModel(InstituteCoordinatorDto instituteCoordinator);

    public static partial CreateInstituteCoordinatorDto MapCreateInstituteCoordinatorFromModel(CreateInstituteCoordinatorModel instituteCoordinator);

    public static partial PatchInstituteCoordinatorDto MapPatchInstituteCoordinatorTypeFromModel(PatchInstituteCoordinatorModel instituteCoordinator);

    [MapProperty(nameof(GetRequest<InstituteCoordinatorFilterModel>.PageSize), nameof(GetInstituteCoordinatorDto.BatchSize))]
    [MapProperty(
        nameof(GetRequest<InstituteCoordinatorFilterModel>.Page),
        nameof(GetInstituteCoordinatorDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetInstituteCoordinatorDto MapGetInstituteCoordinatorFromModel(GetRequest<InstituteCoordinatorFilterModel> model);
}
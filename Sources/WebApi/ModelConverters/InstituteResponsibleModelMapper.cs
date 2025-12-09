using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Riok.Mapperly.Abstractions;
using WebApi.Models.InstituteCoordinator;
using WebApi.Models.Requests.InstituteResponsible;
// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteResponsibleModelMapper
{
    public static partial InstituteCoordinatorModel MapInstituteCoordinatorToModel(InstituteCoordinatorDto instituteCoordinator);

    [MapperIgnoreSource(nameof(InstituteCoordinatorDto.Id))]
    public static partial PatchInstituteCoordinatorModel MapInstituteCoordinatorToPatchModel(InstituteCoordinatorDto instituteCoordinator);

    public static partial CreateInstituteCoordinatorDto MapCreateInstituteCoordinatorFromModel(CreateInstituteResponsibleModel instituteResponsible);

    public static partial PatchInstituteCoordinatorDto MapPatchInstituteCoordinatorTypeFromModel(PatchInstituteCoordinatorModel instituteCoordinator);

    [MapProperty(nameof(GetInstituteResponsibleModel.AfterInstituteResponsibleId), nameof(GetInstituteCoordinatorDto.AfterInstituteResponsibleId))]
    [MapProperty(nameof(GetInstituteResponsibleModel.PageSize), nameof(GetInstituteCoordinatorDto.BatchSize))]
    [MapProperty(
        nameof(GetInstituteResponsibleModel.Page),
        nameof(GetInstituteCoordinatorDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetInstituteCoordinatorDto MapGetInstituteCoordinatorFromModel(GetInstituteResponsibleModel model);
}
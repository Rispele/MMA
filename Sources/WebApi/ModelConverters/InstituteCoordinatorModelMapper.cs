using Booking.Core.Interfaces.Dtos.InstituteCoordinator;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Riok.Mapperly.Abstractions;
using WebApi.Models.InstituteCoordinator;
using WebApi.Models.Requests.InstituteResponsible;
// ReSharper disable RedundantVerbatimPrefix

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteCoordinatorModelMapper
{
    public static partial InstituteCoordinatorModel MapInstituteCoordinatorToModel(InstituteCoordinatorDto instituteCoordinator);

    [MapperIgnoreSource(nameof(InstituteCoordinatorDto.Id))]
    public static partial PatchInstituteCoordinatorModel MapInstituteCoordinatorToPatchModel(InstituteCoordinatorDto instituteCoordinator);

    public static partial CreateInstituteCoordinatorDto MapCreateInstituteCoordinatorFromModel(CreateInstituteCoordinatorModel instituteCoordinator);

    public static partial PatchInstituteCoordinatorDto MapPatchInstituteCoordinatorTypeFromModel(PatchInstituteCoordinatorModel instituteCoordinator);

    [MapProperty(nameof(GetInstituteCoordinatorModel.AfterInstituteResponsibleId), nameof(GetInstituteCoordinatorDto.AfterInstituteResponsibleId))]
    [MapProperty(nameof(GetInstituteCoordinatorModel.PageSize), nameof(GetInstituteCoordinatorDto.BatchSize))]
    [MapProperty(
        nameof(GetInstituteCoordinatorModel.Page),
        nameof(GetInstituteCoordinatorDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetInstituteCoordinatorDto MapGetInstituteCoordinatorFromModel(GetInstituteCoordinatorModel model);
}
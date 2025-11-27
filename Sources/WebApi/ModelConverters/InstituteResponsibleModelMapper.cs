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
    public static partial InstituteCoordinatorModel MapInstituteResponsibleToModel(InstituteCoordinatorDto instituteCoordinator);

    [MapperIgnoreSource(nameof(InstituteCoordinatorDto.Id))]
    public static partial PatchInstituteResponsibleModel MapInstituteResponsibleToPatchModel(InstituteCoordinatorDto instituteCoordinator);

    public static partial CreateInstituteCoordinatorDto MapCreateInstituteResponsibleFromModel(CreateInstituteResponsibleModel instituteResponsible);

    public static partial PatchInstituteCoordinatorDto MapPatchInstituteResponsibleTypeFromModel(PatchInstituteResponsibleModel instituteResponsible);

    public static partial InstituteCoordinatorDto MapInstituteResponsibleFromModel(InstituteCoordinatorModel instituteCoordinator);

    [MapProperty(nameof(GetInstituteResponsibleModel.AfterInstituteResponsibleId), nameof(GetInstituteCoordinatorDto.AfterInstituteResponsibleId))]
    [MapProperty(nameof(GetInstituteResponsibleModel.PageSize), nameof(GetInstituteCoordinatorDto.BatchSize))]
    [MapProperty(
        nameof(GetInstituteResponsibleModel.Page),
        nameof(GetInstituteCoordinatorDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetInstituteCoordinatorDto MapGetInstituteResponsibleFromModel(GetInstituteResponsibleModel model);
}
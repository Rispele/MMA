using Riok.Mapperly.Abstractions;
using Rooms.Core.Dtos.InstituteResponsible;
using Rooms.Core.Dtos.Requests.InstituteResponsible;
using WebApi.Models.InstituteResponsible;
using WebApi.Models.Requests.InstituteResponsible;

namespace WebApi.ModelConverters;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class InstituteResponsibleModelMapper
{
    public static partial InstituteResponsibleModel MapInstituteResponsibleToModel(InstituteResponsibleDto instituteResponsible);

    [MapperIgnoreSource(nameof(InstituteResponsibleDto.Id))]
    public static partial PatchInstituteResponsibleModel MapInstituteResponsibleToPatchModel(InstituteResponsibleDto instituteResponsible);

    public static partial CreateInstituteResponsibleDto MapCreateInstituteResponsibleFromModel(CreateInstituteResponsibleModel instituteResponsible);

    public static partial PatchInstituteResponsibleDto MapPatchInstituteResponsibleTypeFromModel(PatchInstituteResponsibleModel instituteResponsible);

    public static partial InstituteResponsibleDto MapInstituteResponsibleFromModel(InstituteResponsibleModel instituteResponsible);

    [MapProperty(nameof(GetInstituteResponsibleModel.AfterInstituteResponsibleId), nameof(GetInstituteResponsibleDto.AfterInstituteResponsibleId))]
    [MapProperty(nameof(GetInstituteResponsibleModel.PageSize), nameof(GetInstituteResponsibleDto.BatchSize))]
    [MapProperty(
        source: nameof(GetInstituteResponsibleModel.Page),
        target: nameof(GetInstituteResponsibleDto.BatchNumber),
        Use = nameof(@PageIndexingConverter.MapPageNumberToBatchNumber))]
    public static partial GetInstituteResponsibleDto MapGetInstituteResponsibleFromModel(GetInstituteResponsibleModel model);
}
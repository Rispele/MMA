using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Queries.Implementations.InstituteResponsible;

namespace Rooms.Core.Queries.Factories;

public interface IInstituteResponsibleQueryFactory
{
    public IFilterInstituteResponsibleQuery Filter(
        int batchSize,
        int batchNumber,
        int afterInstituteResponsibleId = -1,
        InstituteResponsibleFilterDto? filter = null);

    public IFindInstituteResponsibleByIdQuery FindById(int instituteResponsibleId);
}
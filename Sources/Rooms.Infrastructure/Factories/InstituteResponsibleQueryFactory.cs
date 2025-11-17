using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Queries.Factories;
using Rooms.Core.Queries.Implementations.InstituteResponsible;
using Rooms.Infrastructure.Queries.InstituteResponsible;

namespace Rooms.Infrastructure.Factories;

public class InstituteResponsibleQueryFactory : IInstituteResponsibleQueryFactory
{
    public IFilterInstituteResponsibleQuery Filter(
        int batchSize,
        int batchNumber,
        int afterInstituteResponsibleId = -1,
        InstituteResponsibleFilterDto? filter = null)
    {
        return new FilterInstituteResponsibleQuery
        {
            BatchSize = batchSize,
            BatchNumber = batchNumber,
            AfterInstituteResponsibleId = afterInstituteResponsibleId,
            Filter = filter
        };
    }

    public IFindInstituteResponsibleByIdQuery FindById(int instituteResponsibleId)
    {
        return new FindInstituteResponsibleByIdQuery
        {
            InstituteResponsibleId = instituteResponsibleId
        };
    }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.InstituteResponsible;
using Rooms.Core.Queries.Implementations.InstituteResponsible;
using Rooms.Infrastructure.Queries.Abstractions;

namespace Rooms.Infrastructure.Queries.InstituteResponsible;

public class FilterInstituteResponsibleQuery :
    IFilterInstituteResponsibleQuery,
    IQueryImplementer<Domain.Models.InstituteResponsible.InstituteResponsible, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterInstituteResponsibleId { get; init; }
    public InstituteResponsibleFilterDto? Filter { get; init; }

    public IAsyncEnumerable<Domain.Models.InstituteResponsible.InstituteResponsible> Apply(RoomsDbContext source)
    {
        IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> instituteResponsible = source.InstituteResponsible;

        instituteResponsible = Filters(instituteResponsible);
        instituteResponsible = Sort(instituteResponsible);
        instituteResponsible = Paginate(instituteResponsible);

        return instituteResponsible.AsAsyncEnumerable();
    }

    private IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> Filters(
        IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> instituteResponsible)
    {
        if (Filter is null)
        {
            return instituteResponsible;
        }

        return null;
    }

    private IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> Sort(
        IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> instituteResponsible)
    {
        if (Filter is null)
        {
            return instituteResponsible;
        }

        (SortDirectionDto? direction, Expression<Func<Domain.Models.InstituteResponsible.InstituteResponsible, object>> parameter)[]
            sorts =
            [
                // BuildSort(Filter.Name?.SortDirection, parameter: t => t.Name)
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return instituteResponsible;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => instituteResponsible.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => instituteResponsible.OrderByDescending(firstSort.parameter),
            _ => throw new ArgumentOutOfRangeException()
        };

        foreach (var (direction, parameter) in sortsToApply.Skip(1))
        {
            orderedQueryable = direction switch
            {
                SortDirectionDto.Ascending => orderedQueryable.ThenBy(parameter),
                SortDirectionDto.Descending => orderedQueryable.ThenByDescending(parameter),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return orderedQueryable;

        (SortDirectionDto? direction, Expression<Func<Domain.Models.InstituteResponsible.InstituteResponsible, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<Domain.Models.InstituteResponsible.InstituteResponsible, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> Paginate(
        IQueryable<Domain.Models.InstituteResponsible.InstituteResponsible> instituteResponsible)
    {
        return instituteResponsible.Where(t => t.Id > AfterInstituteResponsibleId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
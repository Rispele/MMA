using System.Linq.Expressions;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.OperatorDepartments;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;
using Rooms.Persistence.Queries.Abstractions;

namespace Rooms.Persistence.Queries.OperatorDepartments;

public class FilterOperatorDepartmentsQuery :
    IFilterOperatorDepartmentsQuery,
    IQueryImplementer<OperatorDepartment, RoomsDbContext>
{
    public required int BatchSize { get; init; }
    public required int BatchNumber { get; init; }
    public int AfterOperatorDepartmentId { get; init; }
    public OperatorDepartmentsFilterDto? Filter { get; init; }

    public IAsyncEnumerable<OperatorDepartment> Apply(RoomsDbContext source)
    {
        IQueryable<OperatorDepartment> operatorDepartments = source.OperatorDepartments.Include(x => x.Rooms);

        operatorDepartments = Filters(operatorDepartments);
        operatorDepartments = Sort(operatorDepartments);
        operatorDepartments = Paginate(operatorDepartments);

        return operatorDepartments.ToAsyncEnumerable();
    }

    private IQueryable<OperatorDepartment> Filters(
        IQueryable<OperatorDepartment> operatorDepartments)
    {
        if (Filter is null)
        {
            return operatorDepartments;
        }

        operatorDepartments = Filter.Name
            .AsOptional()
            .Apply(operatorDepartments,
                apply: (queryable, parameter) => { return queryable.Where(t => t.Name != null! && t.Name.Contains(parameter.Value)); });

        return operatorDepartments;
    }

    private IQueryable<OperatorDepartment> Sort(
        IQueryable<OperatorDepartment> operatorDepartments)
    {
        if (Filter is null)
        {
            return operatorDepartments;
        }

        (SortDirectionDto? direction, Expression<Func<OperatorDepartment, object>> parameter)[]
            sorts =
            [
                BuildSort(Filter.Name?.SortDirection, parameter: t => t.Name)
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return operatorDepartments;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => operatorDepartments.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => operatorDepartments.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<OperatorDepartment, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<OperatorDepartment, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<OperatorDepartment> Paginate(
        IQueryable<OperatorDepartment> operatorDepartments)
    {
        return operatorDepartments.Where(t => t.Id > AfterOperatorDepartmentId).Skip(BatchSize * BatchNumber).Take(BatchSize);
    }
}
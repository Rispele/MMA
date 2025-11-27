using System.Linq.Expressions;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

internal class FilterOperatorDepartmentsQueryHandler : IQueryHandler<RoomsDbContext, FilterOperatorDepartmentsQuery, OperatorDepartment>
{
    public Task<IAsyncEnumerable<OperatorDepartment>> Handle(
        EntityQuery<RoomsDbContext, FilterOperatorDepartmentsQuery, IAsyncEnumerable<OperatorDepartment>> request,
        CancellationToken cancellationToken)
    {
        IQueryable<OperatorDepartment> operatorDepartments = request.Context.OperatorDepartments.Include(x => x.Rooms);

        operatorDepartments = Filters(operatorDepartments, request.Query.Filter);
        operatorDepartments = Sort(operatorDepartments, request.Query.Filter);
        operatorDepartments = Paginate(operatorDepartments, request.Query);

        return Task.FromResult(operatorDepartments.AsAsyncEnumerable());
    }

    private IQueryable<OperatorDepartment> Filters(IQueryable<OperatorDepartment> operatorDepartments, OperatorDepartmentsFilterDto? filter)
    {
        if (filter is null)
        {
            return operatorDepartments;
        }

        operatorDepartments = filter.Name
            .AsOptional()
            .Apply(operatorDepartments,
                apply: (queryable, parameter) => { return queryable.Where(t => t.Name != null! && t.Name.Contains(parameter.Value)); });

        return operatorDepartments;
    }

    private IQueryable<OperatorDepartment> Sort(IQueryable<OperatorDepartment> operatorDepartments, OperatorDepartmentsFilterDto? filter)
    {
        if (filter is null)
        {
            return operatorDepartments;
        }

        (SortDirectionDto? direction, Expression<Func<OperatorDepartment, object>> parameter)[]
            sorts =
            [
                BuildSort(filter.Name?.SortDirection, parameter: t => t.Name)
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

    private IQueryable<OperatorDepartment> Paginate(IQueryable<OperatorDepartment> operatorDepartments, FilterOperatorDepartmentsQuery requestQuery)
    {
        return operatorDepartments
            .Where(t => t.Id > requestQuery.AfterOperatorDepartmentId)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
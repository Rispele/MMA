using System.Linq.Expressions;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments.Requests;
using Rooms.Core.Queries.Implementations.OperatorDepartments;
using Rooms.Domain.Models.OperatorDepartments;

namespace Rooms.Infrastructure.EFCore.QueryHandlers.OperatorDepartments;

internal class FilterOperatorDepartmentsQueryHandler : IPaginatedQueryHandler<RoomsDbContext, FilterOperatorDepartmentsQuery, OperatorDepartment>
{
    public Task<(IAsyncEnumerable<OperatorDepartment>, int)> Handle(
        EntityQuery<RoomsDbContext, FilterOperatorDepartmentsQuery, (IAsyncEnumerable<OperatorDepartment>, int)> request,
        CancellationToken cancellationToken)
    {
        IQueryable<OperatorDepartment> operatorDepartments = request.Context.OperatorDepartments.Include(x => x.Rooms);

        operatorDepartments = Filters(operatorDepartments, request.Query.Filter);
        operatorDepartments = Sort(operatorDepartments, request.Query.Filter);
        operatorDepartments = Paginate(operatorDepartments, request.Query);

        return Task.FromResult((operatorDepartments.AsAsyncEnumerable(), request.Context.OperatorDepartments.Count()));
    }

    private IQueryable<OperatorDepartment> Filters(IQueryable<OperatorDepartment> operatorDepartments, OperatorDepartmentsFilterDto? filter)
    {
        if (filter is null)
        {
            return operatorDepartments;
        }

        if (filter.Name != null)
        {
            operatorDepartments = filter.Name
                .AsOptional()
                .Apply(operatorDepartments,
                    apply: (queryable, parameter) =>
                        queryable.Where(t => t.Name != null! && t.Name.ToLower().Contains(parameter.Value.ToLower())));
        }

        // if (filter.RoomName != null)
        // {
        //     operatorDepartments = filter.RoomName
        //         .AsOptional()
        //         .Apply(operatorDepartments,
        //             apply: (queryable, parameter) =>
        //                 queryable.Where(t => t.Rooms.Any(x => x.Name.ToLower().Contains(parameter.Value.ToLower()))));
        // }

        // if (filter.Operator != null)
        // {
        //     operatorDepartments = filter.Operator
        //         .AsOptional()
        //         .Apply(operatorDepartments,
        //             apply: (queryable, parameter) =>
        //                 queryable.Where(t => t.Operators.Values.Any(x => x.ToLower().Contains(parameter.Value.ToLower()))));
        // }

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
            .OrderBy(t => t.Id)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
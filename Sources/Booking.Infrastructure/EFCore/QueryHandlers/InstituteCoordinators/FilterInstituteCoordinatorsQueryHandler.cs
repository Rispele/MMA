using System.Linq.Expressions;
using Booking.Core.Interfaces.Dtos.InstituteCoordinator.Requests;
using Booking.Core.Queries.InstituteCoordinators;
using Booking.Domain.Models.InstituteCoordinators;
using Commons.Core.Models.Filtering;
using Commons.Infrastructure.EFCore.QueryHandlers;
using Commons.Optional;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.EFCore.QueryHandlers.InstituteCoordinators;

internal class FilterInstituteCoordinatorsQueryHandler : IPaginatedQueryHandler<BookingDbContext, FilterInstituteCoordinatorsQuery, InstituteCoordinator>
{
    public Task<(IAsyncEnumerable<InstituteCoordinator>, int)> Handle(EntityQuery<BookingDbContext, FilterInstituteCoordinatorsQuery, (IAsyncEnumerable<InstituteCoordinator>, int)> request, CancellationToken cancellationToken)
    {
        IQueryable<InstituteCoordinator> instituteCoordinators = request.Context.InstituteCoordinators;

        instituteCoordinators = Filters(instituteCoordinators, request.Query.Filter);
        instituteCoordinators = Sort(instituteCoordinators, request.Query.Filter);
        instituteCoordinators = Paginate(instituteCoordinators, request.Query);

        return Task.FromResult((instituteCoordinators.AsAsyncEnumerable(), request.Context.InstituteCoordinators.Count()));
    }

    private IQueryable<InstituteCoordinator> Filters(IQueryable<InstituteCoordinator> instituteCoordinators, InstituteCoordinatorFilterDto? filter)
    {
        if (filter is null)
        {
            return instituteCoordinators;
        }

        if (filter.InstituteId != null)
        {
            instituteCoordinators = filter.InstituteId
                .AsOptional()
                .Apply(instituteCoordinators,
                    apply: (queryable, parameter) => queryable
                        .Where(t => t.InstituteId == parameter.Value));
        }

        // if (filter.Coordinator != null)
        // {
        //     instituteCoordinators = filter.Coordinator
        //         .AsOptional()
        //         .Apply(instituteCoordinators,
        //             apply: (queryable, parameter) => queryable
        //                 .Where(t => t.Coordinators.Count > 0));
        // }

        return instituteCoordinators;
    }

    private IQueryable<InstituteCoordinator> Sort(IQueryable<InstituteCoordinator> instituteCoordinators, InstituteCoordinatorFilterDto? filter)
    {
        if (filter is null)
        {
            return instituteCoordinators;
        }

        (SortDirectionDto? direction, Expression<Func<InstituteCoordinator, object>> parameter)[]
            sorts =
            [
                BuildSort(filter.InstituteId?.SortDirection, parameter: t => t.InstituteId),
                BuildSort(filter.Coordinator?.SortDirection, parameter: t => t.Coordinators)
            ];

        var sortsToApply = sorts.Where(t => t.direction is not (null or SortDirectionDto.None)).ToArray();
        if (sortsToApply.Length == 0)
        {
            return instituteCoordinators;
        }

        var firstSort = sortsToApply.FirstOrDefault();
        var orderedQueryable = firstSort.direction switch
        {
            SortDirectionDto.Ascending => instituteCoordinators.OrderBy(firstSort.parameter),
            SortDirectionDto.Descending => instituteCoordinators.OrderByDescending(firstSort.parameter),
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

        (SortDirectionDto? direction, Expression<Func<InstituteCoordinator, object>>) BuildSort(
            SortDirectionDto? direction,
            Expression<Func<InstituteCoordinator, object>> parameter)
        {
            return (direction, parameter);
        }
    }

    private IQueryable<InstituteCoordinator> Paginate(IQueryable<InstituteCoordinator> instituteCoordinators, FilterInstituteCoordinatorsQuery requestQuery)
    {
        return instituteCoordinators
            .OrderBy(t => t.Id)
            .Skip(requestQuery.BatchSize * requestQuery.BatchNumber)
            .Take(requestQuery.BatchSize);
    }
}
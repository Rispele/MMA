using Booking.Core.Queries.InstituteCoordinators;
using Booking.Domain.Models.InstituteCoordinators;
using Commons.Infrastructure.EFCore.QueryHandlers;

namespace Booking.Infrastructure.EFCore.QueryHandlers.InstituteCoordinators;

internal class FilterInstituteCoordinatorsQueryHandler : IQueryHandler<BookingDbContext, FilterInstituteCoordinatorsQuery, InstituteCoordinator>
{
    public Task<IAsyncEnumerable<InstituteCoordinator>> Handle(EntityQuery<BookingDbContext, FilterInstituteCoordinatorsQuery, IAsyncEnumerable<InstituteCoordinator>> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
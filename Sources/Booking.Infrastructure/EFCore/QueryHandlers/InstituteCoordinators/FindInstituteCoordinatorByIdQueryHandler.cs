using Booking.Core.Queries.InstituteCoordinators;
using Booking.Domain.Models.InstituteCoordinators;
using Commons.Infrastructure.EFCore.QueryHandlers;

namespace Booking.Infrastructure.EFCore.QueryHandlers.InstituteCoordinators;

internal class FindInstituteCoordinatorByIdQueryHandler : ISingleQueryHandler<BookingDbContext, FindInstituteCoordinatorByIdQuery, InstituteCoordinator>
{
    public Task<InstituteCoordinator> Handle(EntityQuery<BookingDbContext, FindInstituteCoordinatorByIdQuery, InstituteCoordinator> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
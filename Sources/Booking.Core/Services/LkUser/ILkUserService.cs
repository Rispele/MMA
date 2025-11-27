using Commons.ExternalClients.LkUsers;

namespace Booking.Core.Services.LkUser;

public interface ILkUserService
{
    public Task<LkEmployeeDto[]> GetTeachers(CancellationToken cancellationToken);
    public Task<LkEmployeeDto[]> GetEmployees(CancellationToken cancellationToken);
}
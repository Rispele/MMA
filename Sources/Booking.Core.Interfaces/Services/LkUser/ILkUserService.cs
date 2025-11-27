using Commons.ExternalClients.LkUsers;

namespace Booking.Core.Interfaces.Services.LkUser;

public interface ILkUserService
{
    public Task<LkEmployeeDto[]> GetTeachers(CancellationToken cancellationToken);
    public Task<LkEmployeeDto[]> GetEmployees(CancellationToken cancellationToken);
}
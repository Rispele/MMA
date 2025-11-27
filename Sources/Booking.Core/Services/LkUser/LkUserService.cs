using Booking.Core.Interfaces.Services.LkUser;
using Commons.ExternalClients.LkUsers;

namespace Booking.Core.Services.LkUser;

public class LkUserService(ILkUsersClient lkUsersClient) : ILkUserService
{
    public async Task<LkEmployeeDto[]> GetTeachers(CancellationToken cancellationToken)
    {
        var employees = await GetEmployees(cancellationToken);
        
        return employees.Where(t => t.Post.Contains("Преподаватель")).ToArray();
    }

    public async Task<LkEmployeeDto[]> GetEmployees(CancellationToken cancellationToken)
    {
        return await lkUsersClient.GetEmployees(cancellationToken);
    }
}
using Rooms.Core.Clients.LkUsers;

namespace Rooms.Core.Services.LkUser;

public interface ILkUserService
{
    public Task<LkEmployeeDto[]> GetTeachers(CancellationToken cancellationToken);
    public Task<LkEmployeeDto[]> GetEmployees(CancellationToken cancellationToken);
}
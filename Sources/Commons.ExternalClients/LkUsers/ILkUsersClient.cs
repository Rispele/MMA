namespace Commons.ExternalClients.LkUsers;

public interface ILkUsersClient
{
    public Task<LkUserDto[]> GetUsers(CancellationToken cancellationToken);
    public Task<LkUserDto[]> GetUsers(IEnumerable<string> userIds, CancellationToken cancellationToken);
    public Task<LkEmployeeDto[]> GetEmployees(CancellationToken cancellationToken);
}
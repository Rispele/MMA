namespace Commons.ExternalClients.LkUsers;

public class TestLkUserClient : ILkUsersClient
{
    private static readonly LkUserDto[] Users = LkUserTestData.GetUsers(); 
    private static readonly LkEmployeeDto[] Employees = LkUserTestData.GetEmployees(); 
    
    public Task<LkUserDto[]> GetUsers(CancellationToken cancellationToken)
    {
        return Task.FromResult(Users);
    }

    public Task<LkEmployeeDto[]> GetEmployees(CancellationToken cancellationToken)
    {
        return Task.FromResult(Employees);
    }
}
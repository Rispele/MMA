using Rooms.Core.Clients.LkUsers;

namespace Rooms.Infrastructure.Clients.LkUser;

public class TestLkUserClient : ILkUsersClient
{
    private static readonly LkUserDto[] Users = LkUserTestData.GetUsers(); 
    
    public Task<LkUserDto[]> GetUsers(CancellationToken cancellationToken)
    {
        return Task.FromResult(Users);
    }
}
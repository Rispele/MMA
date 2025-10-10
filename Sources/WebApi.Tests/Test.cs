using FluentAssertions;
using WebApi.Tests.SDK;
using WebApi.Tests.TestingInfrastructure;

namespace WebApi.Tests;

public class Tests : ContainerTestBase
{
    [Inject]
    private readonly RoomsSdk roomsSdk = null!;
    
    [Test]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomsSdk.CreateRoom(roomName);

        created.Name.Should().Be(roomName);
    }
}
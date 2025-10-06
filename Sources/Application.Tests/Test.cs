using Application.Clients.Dtos.Requests.RoomCreating;
using Application.Clients.Implementations;
using FluentAssertions;
using Sources.AppHost;

namespace Application.Tests;

public class Tests
{
    private static TestingApplicationFactory Factory => ApplicationTestsContext.TestingApplicationFactory;
    private static readonly RoomsClient RoomsClient = new(Factory.CreateHttpClient(resourceName: KnownResourceNames.ApplicationService));

    [Test]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        var roomName = Guid.NewGuid().ToString();
        var created = await RoomsClient.CreateRoomAsync(new CreateRoomRequest { Name = roomName });

        // Assert
        created.Name.Should().Be(roomName);
    }
}
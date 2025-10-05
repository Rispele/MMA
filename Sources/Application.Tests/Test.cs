using Application.Clients.Dtos.Requests.RoomCreating;
using Application.Clients.Implementations;
using FluentAssertions;

namespace Application.Tests;

public class Tests
{
    private ApplicationTestingHostFactory factory;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        factory = new ApplicationTestingHostFactory();

        await factory.StartAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await factory.DisposeAsync();
    }

    [Test]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        var roomsClient = new RoomsClient(factory.CreateHttpClient("application"));
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await factory.Application.ResourceNotifications.WaitForResourceHealthyAsync("application", cts.Token);

        var roomName = Guid.NewGuid().ToString();
        var created = await roomsClient.CreateRoomAsync(new CreateRoomRequest { Name = roomName }, cts.Token);

        // Assert
        created.Name.Should().Be(roomName);
    }
}
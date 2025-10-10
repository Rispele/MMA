using FluentAssertions;

namespace Application.Tests;

public class Tests
{
    [Test]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await ApplicationTestsContext.RoomsSdk.CreateRoom(roomName);

        created.Name.Should().Be(roomName);
    }
}
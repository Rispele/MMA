using FluentAssertions;
using Rooms.Domain.Exceptions;
using WebApi.Tests.SDK;
using WebApi.Tests.TestingInfrastructure;

namespace WebApi.Tests;

public class RoomsApiTests : ContainerTestBase
{
    [Inject] private readonly RoomsSdk roomsSdk = null!;

    [Test]
    public async Task CreateRoom_ShouldBeCreated()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomsSdk.CreateRoom(roomName);

        created.Name.Should().Be(roomName);
    }

    [Test]
    public async Task GetRoom_ById_ForExistent_ShouldBeFound()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomsSdk.CreateRoom(
            roomName,
            b => b.Comment("comment"));
        var found = await roomsSdk.GetRoom(created.Id);

        created.Should().BeEquivalentTo(found);
    }

    [Test]
    public async Task GetRoom_ById_ForNotExistent_ShouldThrow()
    {
        var action = () => roomsSdk.GetRoom(-10);

        await action.Should().ThrowAsync<RoomNotFoundException>();
    }

    [Test]
    public async Task FilterRoom_ByComment_ShouldReturnCorrect()
    {
        var room1Name = Guid.NewGuid().ToString();
        var room2Name = Guid.NewGuid().ToString();
        var room3Name = Guid.NewGuid().ToString();

        var comment1 = Guid.NewGuid().ToString();
        var comment2 = Guid.NewGuid().ToString();

        var room1 = await roomsSdk.CreateRoom(room1Name, builder: builder => builder.Comment(comment1));
        var room2 = await roomsSdk.CreateRoom(room2Name, builder: builder => builder.Comment(comment2));
        var room3 = await roomsSdk.CreateRoom(room3Name, builder: builder => builder.Comment(comment1));

        var response = await roomsSdk.FilterRooms(builder: b => b
            .Filter(f => f.Comment(comment1)));

        response.Rooms.Should().HaveCount(2);
        response.Rooms.Should().ContainInConsecutiveOrder(room1, room3);
        response.Rooms.Should().NotContainEquivalentOf(room2);
    }
}
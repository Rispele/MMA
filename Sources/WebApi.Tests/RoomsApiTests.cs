using FluentAssertions;
using Rooms.Core.Dtos.Room;
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

    [TestCaseSource(nameof(FilterRoomTestCases))]
    public async Task FilterRoom_ByComment_ShouldReturnCorrect(
        Action<CreateRoomRequestBuilder> createRoomOfParameter1,
        Action<CreateRoomRequestBuilder> createRoomOfParameter2,
        Action<FilterRoomsRequestBuilder> filterRoomByParameter1)
    {
        var room1Name = Guid.NewGuid().ToString();
        var room2Name = Guid.NewGuid().ToString();
        var room3Name = Guid.NewGuid().ToString();

        var room1 = await roomsSdk.CreateRoom(room1Name, builder: createRoomOfParameter1);
        var room2 = await roomsSdk.CreateRoom(room2Name, builder: createRoomOfParameter1);
        var room3 = await roomsSdk.CreateRoom(room3Name, builder: createRoomOfParameter2);

        var response = await roomsSdk.FilterRooms(builder: filterRoomByParameter1);

        response.Rooms.Should().HaveCount(2);
        response.Rooms.Should().ContainInConsecutiveOrder(room1, room2);
        response.Rooms.Should().NotContainEquivalentOf(room3);
    }

    [Test]
    public async Task FilterRoom_Numbers_ShouldReturnGreatOfEqual()
    {
        var room1Name = Guid.NewGuid().ToString();
        var room2Name = Guid.NewGuid().ToString();

        var room1 = await roomsSdk.CreateRoom(room1Name, builder: b => b.Seats(10));
        var room2 = await roomsSdk.CreateRoom(room2Name, builder: b => b.Seats(5));

        var response = await roomsSdk.FilterRooms(builder: b => b.Filter(f => f.Seats(9)));

        response.Rooms.Should().HaveCountGreaterThanOrEqualTo(1);
        response.Rooms.Should().ContainEquivalentOf(room1);
        response.Rooms.Should().NotContainEquivalentOf(room2);
        response.Rooms.Select(t => t.Parameters.Seats).Min().Should().BeGreaterThanOrEqualTo(9);
    }

    [Test]
    public async Task PatchRoom_ShouldBePatched()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomsSdk.CreateRoom(
            roomName,
            b => b.Comment("comment"));

        var patched = await roomsSdk.PatchRoom(
            created.Id,
            b => b.Comment("comment1"));
        
        var found = await roomsSdk.GetRoom(created.Id);

        found.Should().NotBeEquivalentTo(created);
        found.Should().BeEquivalentTo(patched);
        found.FixStatus.Comment.Should().BeEquivalentTo("comment1");
    }

    private static IEnumerable<TestCaseData> FilterRoomTestCases()
    {
        var comment11 = Guid.NewGuid().ToString();
        var comment12 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment11)),
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment12)),
                (Action<FilterRoomsRequestBuilder>)(b => b.Filter(f => f.Comment(comment11))))
            .SetName("String: Full match");

        var comment21 = Guid.NewGuid().ToString();
        var comment22 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment21)),
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment22)),
                (Action<FilterRoomsRequestBuilder>)(b => b.Filter(f => f.Comment(comment21[3..^3]))))
            .SetName("String: Substring match");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Computer)),
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Multimedia)),
                (Action<FilterRoomsRequestBuilder>)(b => b.Filter(f => f.Type([RoomTypeDto.Computer]))))
            .SetName("Enum: Room type");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wired)),
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wireless)),
                (Action<FilterRoomsRequestBuilder>)(b => b.Filter(f => f.NetType([RoomNetTypeDto.Wired]))))
            .SetName("Enum: Net type");


        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Amphitheater)),
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Flat)),
                (Action<FilterRoomsRequestBuilder>)(b => b.Filter(f => f.Layout([RoomLayoutDto.Amphitheater]))))
            .SetName("Enum: Layout type");
    }
}
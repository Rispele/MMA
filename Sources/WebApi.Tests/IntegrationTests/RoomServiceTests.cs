using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.JsonPatch;
using Rooms.Core.Dtos.Room;
using Rooms.Domain.Exceptions;
using WebApi.Models.Requests.Filtering;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;
using WebApi.Services.Interfaces;
using WebApi.Tests.SDK;
using WebApi.Tests.TestingInfrastructure;

namespace WebApi.Tests.IntegrationTests;

public class RoomServiceTests : ContainerTestBase
{
    [Inject]
    private readonly IRoomService roomService = null!;

    [Inject]
    private readonly RoomsSdk roomsSdk = null!;

    [Inject]
    private readonly IMapper mapper = null!;

    [Test]
    public async Task CreateRoom_ShouldBeCreated()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomService.CreateRoom(
            new CreateRoomModel { Name = roomName },
            CancellationToken.None);

        created.Name.Should().Be(roomName);
    }

    [Test]
    public async Task GetRoom_ById_ForExistent_ShouldBeFound()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomsSdk.CreateRoom(
            roomName,
            builder: b => b.Comment("comment"));
        var found = await roomService.GetRoomByIdAsync(created.Id, CancellationToken.None);

        created.Should().BeEquivalentTo(found);
    }

    [Test]
    public async Task GetRoom_ById_ForNotExistent_ShouldThrow()
    {
        var action = () => roomService.GetRoomByIdAsync(id: -10, CancellationToken.None);

        await action.Should().ThrowAsync<RoomNotFoundException>();
    }

    [TestCaseSource(nameof(FilterRoomTestCases))]
    public async Task FilterRoom_ByComment_ShouldReturnCorrect(
        Action<CreateRoomRequestBuilder> createRoomOfParameter1,
        Action<CreateRoomRequestBuilder> createRoomOfParameter2,
        GetRoomsModel roomsRequest)
    {
        var room1Name = Guid.NewGuid().ToString();
        var room2Name = Guid.NewGuid().ToString();
        var room3Name = Guid.NewGuid().ToString();

        var room1 = await roomsSdk.CreateRoom(room1Name, createRoomOfParameter1);
        var room2 = await roomsSdk.CreateRoom(room2Name, createRoomOfParameter1);
        var room3 = await roomsSdk.CreateRoom(room3Name, createRoomOfParameter2);

        var response = await roomService.GetRoomsAsync(roomsRequest, CancellationToken.None);
        var room1Model = mapper.Map<RoomModel>(room1);
        var room2Model = mapper.Map<RoomModel>(room2);
        var room3Model = mapper.Map<RoomModel>(room3);

        response.Rooms.Should().HaveCount(2);
        response.Rooms[0].Should().BeEquivalentTo(room1Model);
        response.Rooms[1].Should().BeEquivalentTo(room2Model);
        response.Rooms.Should().NotContainEquivalentOf(room3Model);
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

    [TestCaseSource(nameof(PatchRoomTestCases))]
    public async Task PatchRoom_ShouldBePatched(
        Action<CreateRoomRequestBuilder> createRoomRequest,
        JsonPatchDocument<PatchRoomModel> patchRoom,
        Action<RoomDto> assertRoom)
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomsSdk.CreateRoom(roomName, createRoomRequest);

        var (result, _) = await roomService.PatchRoomAsync(
            created.Id,
            patchRoom,
            validate: _ => true,
            CancellationToken.None);
        var found = await roomsSdk.GetRoom(created.Id);

        found.Should().NotBeEquivalentTo(created);
        found.Should().BeEquivalentTo(result);
        assertRoom(found);
    }

    private static IEnumerable<TestCaseData> FilterRoomTestCases()
    {
        var comment11 = Guid.NewGuid().ToString();
        var comment12 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment11)),
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment12)),
                new GetRoomsModel
                {
                    PageSize = 10,
                    Filter = new RoomsFilterModel { Comment = new FilterParameterModel<string> { Value = comment11 } }
                })
            .SetName("String: Full match");

        var comment21 = Guid.NewGuid().ToString();
        var comment22 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment21)),
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment22)),
                new GetRoomsModel
                {
                    PageSize = 10,
                    Filter = new RoomsFilterModel
                    {
                        Comment = new FilterParameterModel<string> { Value = comment21[3..^3] }
                    }
                })
            .SetName("String: Substring match");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Computer)),
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Multimedia)),
                new GetRoomsModel
                {
                    PageSize = 10,
                    Filter = new RoomsFilterModel
                    {
                        RoomTypes = new FilterMultiParameterModel<RoomTypeModel> { Values = [RoomTypeModel.Computer] }
                    }
                })
            .SetName("Enum: Room type");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wired)),
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wireless)),
                new GetRoomsModel
                {
                    PageSize = 10,
                    Filter = new RoomsFilterModel
                    {
                        NetTypes = new FilterMultiParameterModel<RoomNetTypeModel> { Values = [RoomNetTypeModel.Wired] }
                    }
                })
            .SetName("Enum: Net type");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Amphitheater)),
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Flat)),
                new GetRoomsModel
                {
                    PageSize = 10,
                    Filter = new RoomsFilterModel
                    {
                        RoomLayout = new FilterMultiParameterModel<RoomLayoutModel>
                        {
                            Values = [RoomLayoutModel.Amphitheater]
                        }
                    }
                })
            .SetName("Enum: Layout type");
    }

    private static IEnumerable<TestCaseData> PatchRoomTestCases()
    {
        var name = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(_ => { }),
                new JsonPatchDocument<PatchRoomModel>().Replace(path: t => t.Name, name),
                (Action<RoomDto>)(t => t.Name.Should().Be(name)))
            .SetName("Name");


        var comment11 = Guid.NewGuid().ToString();
        var comment12 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment11)),
                new JsonPatchDocument<PatchRoomModel>().Replace(path: t => t.Comment, comment12),
                (Action<RoomDto>)(t => t.FixStatus.Comment.Should().Be(comment12)))
            .SetName("FixStatus.Comment");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.RoomStatus(RoomStatusDto.NotReady)),
                new JsonPatchDocument<PatchRoomModel>().Replace(path: t => t.RoomStatus, RoomStatusModel.Ready),
                (Action<RoomDto>)(b => b.FixStatus.Status.Should().Be(RoomStatusDto.Ready)))
            .SetName("FixStatus.Status");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Computer)),
                new JsonPatchDocument<PatchRoomModel>().Replace(path: t => t.Type, RoomTypeModel.Multimedia),
                (Action<RoomDto>)(b => b.Parameters.Type.Should().Be(RoomTypeDto.Multimedia)))
            .SetName("Parameters.Type");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wired)),
                new JsonPatchDocument<PatchRoomModel>().Replace(path: t => t.NetType, RoomNetTypeModel.Wireless),
                (Action<RoomDto>)(b => b.Parameters.NetType.Should().Be(RoomNetTypeDto.Wireless)))
            .SetName("Parameters.NetType");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Amphitheater)),
                new JsonPatchDocument<PatchRoomModel>().Replace(path: t => t.Layout, RoomLayoutModel.Flat),
                (Action<RoomDto>)(b => b.Parameters.Layout.Should().Be(RoomLayoutDto.Flat)))
            .SetName("Parameters.Layout");
    }
}
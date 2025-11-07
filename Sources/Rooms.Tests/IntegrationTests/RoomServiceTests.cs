using FluentAssertions;
using IntegrationTestInfrastructure;
using IntegrationTestInfrastructure.ContainerBasedTests;
using Rooms.Core.Dtos.Requests.Filtering;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Fix;
using Rooms.Core.Dtos.Room.Parameters;
using Rooms.Core.Services.Interfaces;
using Rooms.Domain.Exceptions;
using WebApi.Tests.SDK;

namespace Rooms.Tests.IntegrationTests;

public class RoomServiceTests : ContainerTestBase
{
    [Inject]
    private readonly IRoomService roomService = null!;

    [Inject]
    private readonly RoomsSdk roomsSdk = null!;

    [Test]
    public async Task CreateRoom_ShouldBeCreated()
    {
        var roomName = Guid.NewGuid().ToString();

        var created = await roomService.CreateRoom(
            new CreateRoomDto { Name = roomName },
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

        var found = await roomService.GetRoomById(created.Id, CancellationToken.None);

        created.Should().BeEquivalentTo(found);
    }

    [Test]
    public async Task GetRoom_ById_ForNotExistent_ShouldThrow()
    {
        var action = () => roomService.GetRoomById(roomId: -10, CancellationToken.None);

        await action.Should().ThrowAsync<RoomNotFoundException>();
    }

    [TestCaseSource(nameof(FilterRoomTestCases))]
    public async Task FilterRoom_ByComment_ShouldReturnCorrect(
        Action<CreateRoomRequestBuilder> createRoomOfParameter1,
        Action<CreateRoomRequestBuilder> createRoomOfParameter2,
        GetRoomsRequestDto roomsRequest)
    {
        var room1Name = Guid.NewGuid().ToString();
        var room2Name = Guid.NewGuid().ToString();
        var room3Name = Guid.NewGuid().ToString();

        var room1 = await roomsSdk.CreateRoom(room1Name, createRoomOfParameter1);
        var room2 = await roomsSdk.CreateRoom(room2Name, createRoomOfParameter1);
        var room3 = await roomsSdk.CreateRoom(room3Name, createRoomOfParameter2);

        var response = await roomService.FilterRooms(roomsRequest, CancellationToken.None);

        response.Rooms.Should().HaveCount(2);
        response.Rooms[0].Should().BeEquivalentTo(room1);
        response.Rooms[1].Should().BeEquivalentTo(room2);
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

        var created = await roomsSdk.CreateRoom(roomName, builder: b => b.Seats(10));

        var result = await roomService.PatchRoom(
            created.Id,
            new PatchRoomDto
            {
                Name = "new name",
                Description = "new description",
                ScheduleAddress = new ScheduleAddressDto("123", "123"),
                Type = RoomTypeDto.Multimedia,
                Layout = RoomLayoutDto.Amphitheater,
                Seats = 12,
                ComputerSeats = 13,
                PdfRoomSchemeFile = null,
                PhotoFile = null,
                NetType = RoomNetTypeDto.WiredAndWireless,
                HasConditioning = true,
                Owner = "new owner",
                RoomStatus = RoomStatusDto.Ready,
                Comment = "new comment",
                FixDeadline = DateTime.Now,
                AllowBooking = false
            },
            CancellationToken.None);
        var found = await roomsSdk.GetRoom(created.Id);

        found.Should().NotBeEquivalentTo(created);
        found.Should().BeEquivalentTo(result);
    }

    private static IEnumerable<TestCaseData> FilterRoomTestCases()
    {
        var comment11 = Guid.NewGuid().ToString();
        var comment12 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment11)),
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment12)),
                new GetRoomsRequestDto
                {
                    BatchNumber = 0,
                    BatchSize = 10,
                    AfterRoomId = -1,
                    Filter = new RoomsFilterDto
                    {
                        Comment = new FilterParameterDto<string>(
                            comment11,
                            SortDirectionDto.Ascending)
                    },
                })
            .SetName("String: Full match");

        var comment21 = Guid.NewGuid().ToString();
        var comment22 = Guid.NewGuid().ToString();
        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment21)),
                (Action<CreateRoomRequestBuilder>)(b => b.Comment(comment22)),
                new GetRoomsRequestDto
                {
                    BatchNumber = 0,
                    BatchSize = 10,
                    AfterRoomId = -1,
                    Filter = new RoomsFilterDto
                    {
                        Comment = new FilterParameterDto<string>(
                            comment21[3..^3],
                            SortDirectionDto.Ascending)
                    },
                })
            .SetName("String: Substring match");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Computer)),
                (Action<CreateRoomRequestBuilder>)(b => b.Type(RoomTypeDto.Multimedia)),
                new GetRoomsRequestDto
                {
                    BatchNumber = 0,
                    BatchSize = 10,
                    AfterRoomId = -1,
                    Filter = new RoomsFilterDto
                    {
                        RoomTypes = new FilterMultiParameterDto<RoomTypeDto>(
                            [RoomTypeDto.Computer],
                            SortDirectionDto.Ascending)
                    },
                })
            .SetName("Enum: Room type");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wired)),
                (Action<CreateRoomRequestBuilder>)(b => b.NetType(RoomNetTypeDto.Wireless)),
                new GetRoomsRequestDto
                {
                    BatchNumber = 0,
                    BatchSize = 10,
                    AfterRoomId = -1,
                    Filter = new RoomsFilterDto
                    {
                        NetTypes = new FilterMultiParameterDto<RoomNetTypeDto>(
                            [RoomNetTypeDto.Wired],
                            SortDirectionDto.Ascending)
                    },
                })
            .SetName("Enum: Net type");

        yield return new TestCaseData(
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Amphitheater)),
                (Action<CreateRoomRequestBuilder>)(b => b.Layout(RoomLayoutDto.Flat)),
                new GetRoomsRequestDto
                {
                    BatchNumber = 0,
                    BatchSize = 10,
                    AfterRoomId = -1,
                    Filter = new RoomsFilterDto
                    {
                        RoomLayout = new FilterMultiParameterDto<RoomLayoutDto>(
                            [RoomLayoutDto.Amphitheater],
                            SortDirectionDto.Ascending)
                    },
                })
            .SetName("Enum: Layout type");
    }
}
using FluentAssertions;
using Rooms.Core.Clients.RoomSchedule;
using Rooms.Core.Dtos.Room;
using Rooms.Infrastructure.Clients.RoomSchedule;

namespace Rooms.Tests.UnitTests.Clients;

[TestFixture]
public class RoomScheduleClientTests
{
    [Test]
    public async Task GetRoomSchedule_ShouldNotThrow_And_ItemsShouldNotBeNull()
    {
        var client = SetupClient();

        var request = new GetRoomScheduleRequest(
            new ScheduleAddressDto("СП501", "Мира, 17а"),
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)));
        
        var response = client.GetRoomSchedule(request, CancellationToken.None);
        await foreach (var item in response)
        {
            item.Should().NotBeNull();
        }
    }

    private static RoomScheduleClient SetupClient()
    {
        var httpClient = new HttpClient();
        var settings = new RoomScheduleClientSettings("https://public-schedule-api.my1.urfu.ru/");

        return new RoomScheduleClient(httpClient, settings);
    }
}
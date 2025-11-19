using FluentAssertions;
using Rooms.Core.DtoMappers;

namespace Rooms.Tests.UnitTests;

[TestFixture]
public class RoomModelsMapperTests
{
    [Test]
    public void Map_Room_To_RoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateRoomDto();
        var domain = RoomMapperTestHelper.CreateRoom();

        var mapped = RoomDtoMapper.Map(domain);
        mapped.Should().BeEquivalentTo(dto);
    }
}
using FluentAssertions;
using Rooms.Core.DtoMappers;

namespace Rooms.Tests.UnitTests.DtoMappers;

[TestFixture]
public class RoomDtoMapperTests
{
    [Test]
    public void Map_Room_To_RoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomTestHelper.CreateRoomDto();
        var domain = RoomTestHelper.CreateRoom();

        var mapped = RoomDtoMapper.Map(domain);
        mapped.Should().BeEquivalentTo(dto);
    }
}
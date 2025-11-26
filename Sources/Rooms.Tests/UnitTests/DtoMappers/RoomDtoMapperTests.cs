using Commons;
using FluentAssertions;
using Rooms.Core.Services.Rooms.Mappers;

namespace Rooms.Tests.UnitTests.DtoMappers;

[TestFixture]
public class RoomDtoMapperTests
{
    [Test]
    public void RoomDtoMapperTest()
    {
        var lines = File.ReadAllLines("C:\\Users\\d.smirnov\\Downloads\\Копия Пользователи для тестового двойника - Пользователи.csv");
        var newLines = lines
            .Select(t => t.Split(','))
            .Select(t =>
            {
                t[0] = Guid.NewGuid().ToString();
                return t.JoinStrings(",");
            })
            .ToArray();
        
        File.WriteAllLines("C:\\Users\\d.smirnov\\Downloads\\Копия Пользователи для тестового двойника - Пользователи1.csv", newLines);
    }
    
    [Test]
    public void Map_Room_To_RoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomTestHelper.CreateRoomDto();
        var domain = RoomTestHelper.CreateRoom();

        var mapped = RoomDtoMapper.Map(domain);
        mapped.Should().BeEquivalentTo(dto);
    }
}
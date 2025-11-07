using FluentAssertions;
using WebApi.ModelConverters;

namespace WebApi.Tests.UnitTests;

[TestFixture]
public class RoomModelsMapperTests
{
    [Test]
    public void Map_RoomDto_To_RoomModel_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateRoomDto();
        var model = RoomMapperTestHelper.CreateRoomModel();

        var mapped = RoomModelsMapper.Map(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_RoomDto_To_PatchRoomModel_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateRoomDto();
        var model = RoomMapperTestHelper.CreatePatchRoomModel();

        var mapped = RoomModelsMapper.MapToPatchRoomModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_PatchRoomModel_To_PatchRoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreatePatchRoomDto();
        var model = RoomMapperTestHelper.CreatePatchRoomModel();

        var mapped = RoomModelsMapper.Map(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_CreateRoomModel_To_CreateRoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateCreateRoomDto();
        var model = RoomMapperTestHelper.CreateCreateRoomModel();

        var mapped = RoomModelsMapper.Map(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_GetRoomsModel_To_GetRoomRequestDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateGetRoomsRequestDto();
        var model = RoomMapperTestHelper.CreateGetRoomsModel();

        var mapped = RoomModelsMapper.Map(model);
        mapped.Should().BeEquivalentTo(dto);
    }
}
using FluentAssertions;
using WebApi.ModelConverters;

namespace WebApi.Tests.UnitTests;

[TestFixture]
public class MapperTests
{
    [Test]
    public void Map_RoomDto_To_RoomModel_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateRoomDto();
        var model = RoomMapperTestHelper.CreateRoomModel();

        var mapped = RoomsModelsConverter.Map(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_RoomDto_To_PatchRoomModel_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateRoomDto();
        var model = RoomMapperTestHelper.CreatePatchRoomModel();

        var mapped = RoomsModelsConverter.MapToPatchRoomModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_PatchRoomModel_To_PatchRoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreatePatchRoomDto();
        var model = RoomMapperTestHelper.CreatePatchRoomModel();

        var mapped = RoomsModelsConverter.Map(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_CreateRoomModel_To_CreateRoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateCreateRoomDto();
        var model = RoomMapperTestHelper.CreateCreateRoomModel();

        var mapped = RoomsModelsConverter.Map(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_GetRoomsModel_To_GetRoomRequestDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapperTestHelper.CreateGetRoomsRequestDto();
        var model = RoomMapperTestHelper.CreateGetRoomsModel();

        var mapped = RoomsModelsConverter.Map(model);
        mapped.Should().BeEquivalentTo(dto);
    }
}

// [
//     new EquipmentDto()
//     {
//         Comment = "comment",
//         Id = 1,
//         InventoryNumber = "123",
//         NetworkEquipmentIp = "123",
//         Room = null!,
//         SchemaDto = new EquipmentSchemaDto()
//         {
//             Equipments = [],
//             EquipmentType = new EquipmentTypeDto()
//             {
//                 Id = 1,
//                 Name = "EquipmentTypeName",
//                 Parameters = [new EquipmentParameterDescriptorDto() { Name = "Parameter", Required = true }]
//             },
//             Id = 3,
//             Name = "EquipmentSchemaName",
//             ParameterValues = new Dictionary<string, string>()
//             {
//                 ["key"] = "value"
//             }
//         }
//     }
// ]
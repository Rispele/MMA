using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Rooms.Core.Dtos.Requests.Rooms;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;
using WebApi.Startup.ConfigurationExtensions;

namespace WebApi.Tests.UnitTests;

[TestFixture]
public class MapsterTests
{
    private readonly IMapper mapper = new ServiceMapper(
        new ServiceCollection().BuildServiceProvider(),
        new TypeAdapterConfig().ConfigureMapster());

    [Test]
    public void Map_RoomDto_To_RoomModel_ShouldCorrectlyMap()
    {
        var dto = RoomMapsterTestHelper.CreateRoomDto();
        var model = RoomMapsterTestHelper.CreateRoomModel();

        var mapped = mapper.Map<RoomModel>(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_RoomDto_To_PatchRoomModel_ShouldCorrectlyMap()
    {
        var dto = RoomMapsterTestHelper.CreateRoomDto();
        var model = RoomMapsterTestHelper.CreatePatchRoomModel();

        var mapped = mapper.Map<PatchRoomModel>(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_PatchRoomModel_To_PatchRoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapsterTestHelper.CreatePatchRoomDto();
        var model = RoomMapsterTestHelper.CreatePatchRoomModel();

        var mapped = mapper.Map<PatchRoomDto>(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_CreateRoomModel_To_CreateRoomDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapsterTestHelper.CreateCreateRoomDto();
        var model = RoomMapsterTestHelper.CreateCreateRoomModel();

        var mapped = mapper.Map<CreateRoomModel>(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_GetRoomsModel_To_GetRoomRequestDto_ShouldCorrectlyMap()
    {
        var dto = RoomMapsterTestHelper.CreateGetRoomsRequestDto();
        var model = RoomMapsterTestHelper.CreateGetRoomsModel();

        var mapped = mapper.Map<GetRoomsRequestDto>(model);
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
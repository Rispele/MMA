using FluentAssertions;
using Rooms.Core.Services.Equipments.Mappers;

namespace Rooms.Tests.UnitTests.DtoMappers;

[TestFixture]
public class EquipmentDtoMapperTests
{
    [Test]
    public void Map_Equipment_To_EquipmentDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentsTestHelper.CreateEquipmentDto();
        var domain = EquipmentsTestHelper.CreateEquipment();

        var mapped = EquipmentDtoMapper.MapEquipmentToDto(domain);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_EquipmentSchema_To_EquipmentSchemaModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentsTestHelper.CreateEquipmentSchemaDto();
        var domain = EquipmentsTestHelper.CreateEquipmentSchema();

        var mapped = EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(domain);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_EquipmentType_To_EquipmentTypeModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentsTestHelper.CreateEquipmentTypeDto();
        var domain = EquipmentsTestHelper.CreateEquipmentType();

        var mapped = EquipmentTypeDtoMapper.MapEquipmentTypeToDto(domain);
        mapped.Should().BeEquivalentTo(dto);
    }
}
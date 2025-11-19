using FluentAssertions;
using Rooms.Core.DtoMappers;

namespace Rooms.Tests.UnitTests;

[TestFixture]
public class EquipmentDtoMapperTests
{
    [Test]
    public void Map_Equipment_To_EquipmentDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentsMapperTestHelper.CreateEquipmentDto();
        var domain = EquipmentsMapperTestHelper.CreateEquipment();

        var mapped = EquipmentDtoMapper.MapEquipmentToDto(domain);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_EquipmentSchema_To_EquipmentSchemaModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentsMapperTestHelper.CreateEquipmentSchemaDto();
        var domain = EquipmentsMapperTestHelper.CreateEquipmentSchema();

        var mapped = EquipmentSchemaDtoMapper.MapEquipmentSchemaToDto(domain);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_EquipmentType_To_EquipmentTypeModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentsMapperTestHelper.CreateEquipmentTypeDto();
        var domain = EquipmentsMapperTestHelper.CreateEquipmentType();

        var mapped = EquipmentTypeDtoMapper.MapEquipmentTypeToDto(domain);
        mapped.Should().BeEquivalentTo(dto);
    }
}
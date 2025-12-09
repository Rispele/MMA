using FluentAssertions;
using WebApi.Core.ModelConverters;
using WebApi.Tests.UnitTests.Mappers.Helpers;

namespace WebApi.Tests.UnitTests.Mappers;

[TestFixture]
public class EquipmentModelsMapperTests
{
    [Test]
    public void Map_EquipmentDto_To_EquipmentModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateEquipmentDto();
        var model = EquipmentModelsMapperTestHelper.CreateEquipmentModel();

        var mapped = EquipmentModelsMapper.MapEquipmentToModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_EquipmentDto_To_PatchModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateEquipmentDto();
        var model = EquipmentModelsMapperTestHelper.CreatePatchEquipmentModel();

        var mapped = EquipmentModelsMapper.MapEquipmentToPatchModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_GetEquipmentModel_To_GetEquipmentDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateGetEquipmentsDto();
        var model = EquipmentModelsMapperTestHelper.CreateGetEquipmentsModel();

        var mapped = EquipmentModelsMapper.MapGetEquipmentFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_CreateEquipmentModel_To_CreateEquipmentDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateCreateEquipmentDto();
        var model = EquipmentModelsMapperTestHelper.CreateCreateEquipmentModel();

        var mapped = EquipmentModelsMapper.MapCreateEquipmentFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_PatchEquipmentModel_To_PatchEquipmentDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreatePatchEquipmentDto();
        var model = EquipmentModelsMapperTestHelper.CreatePatchEquipmentModel();

        var mapped = EquipmentModelsMapper.MapPatchEquipmentFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_EquipmentSchemaDto_To_EquipmentSchemaModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateEquipmentSchemaDto();
        var model = EquipmentModelsMapperTestHelper.CreateEquipmentSchemaModel();

        var mapped = EquipmentSchemaModelsMapper.MapEquipmentSchemaToModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_EquipmentSchemaDto_To_PatchModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateEquipmentSchemaDto();
        var model = EquipmentModelsMapperTestHelper.CreatePatchEquipmentSchemaModel();

        var mapped = EquipmentSchemaModelsMapper.MapEquipmentSchemaToPatchModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_GetEquipmentSchemaModel_To_GetEquipmentSchemasDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateGetEquipmentSchemasDto();
        var model = EquipmentModelsMapperTestHelper.CreateGetEquipmentSchemasModel();

        var mapped = EquipmentSchemaModelsMapper.MapGetEquipmentSchemaFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_CreateEquipmentSchemaModel_To_CreateEquipmentSchemaDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateCreateEquipmentSchemaDto();
        var model = EquipmentModelsMapperTestHelper.CreateCreateEquipmentSchemaModel();

        var mapped = EquipmentSchemaModelsMapper.MapCreateEquipmentSchemaFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_PatchEquipmentSchemaModel_To_PatchEquipmentSchemaDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreatePatchEquipmentSchemaDto();
        var model = EquipmentModelsMapperTestHelper.CreatePatchEquipmentSchemaModel();

        var mapped = EquipmentSchemaModelsMapper.MapPatchEquipmentSchemaFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_EquipmentTypeDto_To_EquipmentTypeModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateEquipmentTypeDto();
        var model = EquipmentModelsMapperTestHelper.CreateEquipmentTypeModel();

        var mapped = EquipmentTypeModelsMapper.MapEquipmentTypeToModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_EquipmentTypeDto_To_PatchModel_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateEquipmentTypeDto();
        var model = EquipmentModelsMapperTestHelper.CreatePatchEquipmentTypeModel();

        var mapped = EquipmentTypeModelsMapper.MapEquipmentTypeToPatchModel(dto);
        mapped.Should().BeEquivalentTo(model);
    }

    [Test]
    public void Map_GetEquipmentTypeModel_To_GetEquipmentTypesDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateGetEquipmentTypesDto();
        var model = EquipmentModelsMapperTestHelper.CreateGetEquipmentTypesModel();

        var mapped = EquipmentTypeModelsMapper.MapGetEquipmentTypesFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_CreateEquipmentTypeModel_To_CreateEquipmentTypeDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreateCreateEquipmentTypeDto();
        var model = EquipmentModelsMapperTestHelper.CreateCreateEquipmentTypeModel();

        var mapped = EquipmentTypeModelsMapper.MapCreateEquipmentTypeFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }

    [Test]
    public void Map_PatchEquipmentTypeModel_To_PatchEquipmentTypeDto_ShouldCorrectlyMap()
    {
        var dto = EquipmentModelsMapperTestHelper.CreatePatchEquipmentTypeDto();
        var model = EquipmentModelsMapperTestHelper.CreatePatchEquipmentTypeModel();

        var mapped = EquipmentTypeModelsMapper.MapPatchEquipmentTypeFromModel(model);
        mapped.Should().BeEquivalentTo(dto);
    }
}
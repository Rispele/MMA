using AutoFixture;
using Commons.Core.Models.Filtering;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Commons.Tests.Helpers.SDK;
using FluentAssertions;
using Commons.Tests.Integration.Infrastructure;
using Commons.Tests.Integration.Infrastructure.ContainerBasedTests;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Queries.Implementations.Equipment;

namespace Rooms.Tests.IntegrationTests.Equipments;

[TestFixture]
public class FilterEquipmentSchemaQueryTests : ContainerTestBase
{
    [Inject]
    private readonly MmrSdk mmrSdk = null!;

    [Inject]
    private readonly IUnitOfWorkFactory unitOfWorkFactory = null!;

    [Test]
    public async Task FilterEquipmentSchema_ByName_ShouldReturnCorrectly()
    {
        var fixture = new Fixture();

        var namePrefix = fixture.Create<string>()[..30];
        var schema1 = await mmrSdk.Equipments.CreateEquipmentSchema(namePrefix + "1");
        _ = await mmrSdk.Equipments.CreateEquipmentSchema(fixture.Create<string>());
        var schema3 = await mmrSdk.Equipments.CreateEquipmentSchema(namePrefix + "2");

        await Test(
            schema1.Id,
            schema3.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentSchemasFilterDto
            {
                Name = new FilterParameterDto<string>(namePrefix, sortDirection)
            });
    }

    [Test]
    public async Task FilterEquipmentSchema_ByEquipmentTypeName_ShouldReturnCorrectly()
    {
        var type1 = await mmrSdk.Equipments.CreateEquipmentType();
        var type2 = await mmrSdk.Equipments.CreateEquipmentType();

        var schema1 = await mmrSdk.Equipments.CreateEquipmentSchema(type1.Id);
        _ = await mmrSdk.Equipments.CreateEquipmentSchema(type2.Id);
        var schema3 = await mmrSdk.Equipments.CreateEquipmentSchema(type1.Id);

        await Test(
            schema1.Id,
            schema3.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentSchemasFilterDto
            {
                EquipmentTypeName = new FilterParameterDto<string>(type1.Name, sortDirection)
            });
    }

    [Test]
    public async Task FilterEquipmentSchema_ByParameterValues_ShouldReturnCorrectly()
    {
        var fixture = new Fixture();

        var parameterKey = fixture.Create<string>();

        var schema1 = await mmrSdk.Equipments.CreateEquipmentSchema(
            parameterValues: new Dictionary<string, string>
            {
                [parameterKey] = fixture.Create<string>(),
                [fixture.Create<string>()] = fixture.Create<string>()
            });
        _ = await mmrSdk.Equipments.CreateEquipmentSchema(
            parameterValues: new Dictionary<string, string>
            {
                [fixture.Create<string>()] = fixture.Create<string>(),
                [fixture.Create<string>()] = fixture.Create<string>()
            });
        var schema3 = await mmrSdk.Equipments.CreateEquipmentSchema(
            parameterValues: new Dictionary<string, string>
            {
                [parameterKey] = fixture.Create<string>(),
                [fixture.Create<string>()] = fixture.Create<string>()
            });

        await Test(
            schema1.Id,
            schema3.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentSchemasFilterDto
            {
                EquipmentParameters = new FilterParameterDto<string>(parameterKey, sortDirection)
            });
    }

    private async Task Test(int expected1, int expected2, Func<SortDirectionDto, EquipmentSchemasFilterDto> filterEquipmentsQuery)
    {
        var unitOfWork = await unitOfWorkFactory.Create();
        var actualAscending = await Filter(unitOfWork, BuildQuery(SortDirectionDto.Ascending));
        var actualDescending = await Filter(unitOfWork, BuildQuery(SortDirectionDto.Descending));

        actualAscending.Should().BeEquivalentTo([expected1, expected2]);
        actualDescending.Should().BeEquivalentTo([expected2, expected1]);
        return;

        FilterEquipmentSchemasQuery BuildQuery(SortDirectionDto sortDirection)
        {
            return new FilterEquipmentSchemasQuery(
                BatchSize: 1000,
                BatchNumber: 0,
                filterEquipmentsQuery(sortDirection));
        }
    }

    private static async Task<int[]> Filter(IUnitOfWork unitOfWork, FilterEquipmentSchemasQuery ascendingQuery)
    {
        return (await unitOfWork.ApplyQuery(ascendingQuery, CancellationToken.None)).Item1
            .ToBlockingEnumerable()
            .Select(t => t.Id)
            .ToArray();
    }
}
using AutoFixture;
using Commons.Core.Models.Filtering;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Commons.Tests.Helpers.SDK;
using FluentAssertions;
using IntegrationTestInfrastructure;
using IntegrationTestInfrastructure.ContainerBasedTests;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Queries.Implementations.Equipment;

namespace Rooms.Tests.IntegrationTests.Equipments;

[TestFixture]
public class FilterEquipmentTypeQueryTests : ContainerTestBase
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
        var type1 = await mmrSdk.Equipments.CreateEquipmentType(namePrefix + "1");
        _ = await mmrSdk.Equipments.CreateEquipmentType(fixture.Create<string>());
        var type2 = await mmrSdk.Equipments.CreateEquipmentType(namePrefix + "2");

        await Test(
            type1.Id,
            type2.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentTypesFilterDto
            {
                Name = new FilterParameterDto<string>(namePrefix, sortDirection)
            });
    }

    private async Task Test(int expected1, int expected2, Func<SortDirectionDto, EquipmentTypesFilterDto> filterEquipmentsQuery)
    {
        var unitOfWork = await unitOfWorkFactory.Create();
        var actualAscending = await Filter(unitOfWork, BuildQuery(SortDirectionDto.Ascending));
        var actualDescending = await Filter(unitOfWork, BuildQuery(SortDirectionDto.Descending));

        actualAscending.Should().BeEquivalentTo([expected1, expected2]);
        actualDescending.Should().BeEquivalentTo([expected2, expected1]);
        return;

        FilterEquipmentTypesQuery BuildQuery(SortDirectionDto sortDirection)
        {
            return new FilterEquipmentTypesQuery(
                BatchSize: 1000,
                BatchNumber: 0,
                AfterEquipmentTypeId: -1,
                filterEquipmentsQuery(sortDirection));
        }
    }

    private static async Task<int[]> Filter(IUnitOfWork unitOfWork, FilterEquipmentTypesQuery ascendingQuery)
    {
        return (await unitOfWork.ApplyQuery(ascendingQuery, CancellationToken.None))
            .ToBlockingEnumerable()
            .Select(t => t.Id)
            .ToArray();
    }
}
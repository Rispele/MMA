using AutoFixture;
using AutoFixture.Dsl;
using Commons.Core.Models.Filtering;
using Commons.Domain.Queries.Abstractions;
using Commons.Domain.Queries.Factories;
using Commons.Tests.Helpers.SDK;
using FluentAssertions;
using Commons.Tests.Integration.Infrastructure;
using Commons.Tests.Integration.Infrastructure.ContainerBasedTests;
using Rooms.Core.Interfaces.Dtos.Equipment;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Queries.Implementations.Equipment;
using Rooms.Domain.Propagated.Equipments;

namespace Rooms.Tests.IntegrationTests.Equipments;

[TestFixture]
public class FilterEquipmentQueryTests : ContainerTestBase
{
    [Inject]
    private readonly MmrSdk mmrSdk = null!;

    [Inject]
    private readonly IUnitOfWorkFactory unitOfWorkFactory = null!;

    [Test]
    public async Task FilterEquipment_ByRoom_ShouldReturnCorrectly()
    {
        var fixture = new Fixture();

        var roomNamePrefix = fixture.Create<string>();
        var context = await GenerateContext(
            fixture,
            room1Name: roomNamePrefix + "1",
            room2Name: roomNamePrefix + "2",
            room3Name: roomNamePrefix + "3");

        await mmrSdk.Equipments.CreateEquipment(GenerateCreationRequest(fixture, context.Equipment2.Room.Id, context.Equipment2.Schema.Id).Create());
        var expected1 =
            await mmrSdk.Equipments.CreateEquipment(GenerateCreationRequest(fixture, context.Equipment1.Room.Id, context.Equipment1.Schema.Id)
                .Create());
        var expected3 =
            await mmrSdk.Equipments.CreateEquipment(GenerateCreationRequest(fixture, context.Equipment1.Room.Id, context.Equipment3.Schema.Id)
                .Create());

        await Test(
            expected1.Id,
            expected3.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentsFilterDto
            {
                Rooms = new FilterMultiParameterDto<int>([context.Equipment1.Room.Id], sortDirection)
            });
    }

    [Test]
    public async Task FilterEquipment_BySchema_ShouldReturnCorrectly()
    {
        var fixture = new Fixture();

        var schemaNamePrefix = fixture.Create<string>();
        var context = await GenerateContext(
            fixture,
            schema1Name: schemaNamePrefix + "1",
            schema2Name: schemaNamePrefix + "2",
            schema3Name: schemaNamePrefix + "3");

        await mmrSdk.Equipments.CreateEquipment(GenerateCreationRequest(fixture, context.Equipment2.Room.Id, context.Equipment2.Schema.Id).Create());
        var expected1 =
            await mmrSdk.Equipments.CreateEquipment(GenerateCreationRequest(fixture, context.Equipment1.Room.Id, context.Equipment1.Schema.Id)
                .Create());
        var expected3 =
            await mmrSdk.Equipments.CreateEquipment(GenerateCreationRequest(fixture, context.Equipment3.Room.Id, context.Equipment1.Schema.Id)
                .Create());

        await Test(
            expected1.Id,
            expected3.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentsFilterDto
            {
                Rooms = new FilterMultiParameterDto<int>(
                    [context.Equipment1.Room.Id, context.Equipment2.Room.Id, context.Equipment3.Room.Id],
                    SortDirectionDto.None),
                Schemas = new FilterMultiParameterDto<int>([context.Equipment1.Schema.Id], sortDirection)
            });
    }

    [Test]
    public async Task FilterEquipment_ByStatus_ShouldReturnCorrectly()
    {
        var fixture = new Fixture();

        var context = await GenerateContext(fixture);

        var expected = fixture.Create<EquipmentStatus>();
        var notExpected = fixture.Create<EquipmentStatus>();
        var (expected1, expected2) = await CreateEquipments(
            fixture,
            context,
            expected1EquipmentBuild: composer => composer.With(t => t.Status, expected),
            expected2EquipmentBuild: composer => composer.With(t => t.Status, expected),
            notExpectedEquipmentBuilder: composer => composer.With(t => t.Status, notExpected));

        await Test(
            expected1.Id,
            expected2.Id,
            filterEquipmentsQuery: sortDirection => new EquipmentsFilterDto
            {
                Rooms = new FilterMultiParameterDto<int>(
                    [context.Equipment1.Room.Id, context.Equipment2.Room.Id, context.Equipment3.Room.Id],
                    SortDirectionDto.None),
                Statuses = new FilterMultiParameterDto<EquipmentStatus>([expected], sortDirection)
            });
    }

    private async Task<(EquipmentDto expected1, EquipmentDto expected2)> CreateEquipments(
        Fixture fixture,
        FilterEquipmentTestContext context,
        Func<IPostprocessComposer<CreateEquipmentDto>, IPostprocessComposer<CreateEquipmentDto>> expected1EquipmentBuild,
        Func<IPostprocessComposer<CreateEquipmentDto>, IPostprocessComposer<CreateEquipmentDto>> expected2EquipmentBuild,
        Func<IPostprocessComposer<CreateEquipmentDto>, IPostprocessComposer<CreateEquipmentDto>> notExpectedEquipmentBuilder)
    {
        var creationRequest1 = expected1EquipmentBuild(GenerateCreationRequest(fixture, context.Equipment1.Room.Id, context.Equipment1.Schema.Id))
            .Create();
        var creationRequest2 = expected2EquipmentBuild(GenerateCreationRequest(fixture, context.Equipment2.Room.Id, context.Equipment2.Schema.Id))
            .Create();
        var creationRequest3 = notExpectedEquipmentBuilder(GenerateCreationRequest(fixture, context.Equipment3.Room.Id, context.Equipment3.Schema.Id))
            .Create();


        var expected1 = await mmrSdk.Equipments.CreateEquipment(creationRequest1);
        await mmrSdk.Equipments.CreateEquipment(creationRequest3);
        var expected2 = await mmrSdk.Equipments.CreateEquipment(creationRequest2);

        return (expected1, expected2);
    }

    private async Task Test(int expected1, int expected2, Func<SortDirectionDto, EquipmentsFilterDto> filterEquipmentsQuery)
    {
        var unitOfWork = await unitOfWorkFactory.Create();
        var actualAscending = await Filter(unitOfWork, BuildQuery(SortDirectionDto.Ascending));
        var actualDescending = await Filter(unitOfWork, BuildQuery(SortDirectionDto.Descending));

        actualAscending.Should().BeEquivalentTo([expected1, expected2]);
        actualDescending.Should().BeEquivalentTo([expected2, expected1]);
        return;

        FilterEquipmentsQuery BuildQuery(SortDirectionDto sortDirection)
        {
            return new FilterEquipmentsQuery(
                BatchSize: 1000,
                BatchNumber: 0,
                AfterEquipmentId: -1,
                filterEquipmentsQuery(sortDirection));
        }
    }

    private static async Task<int[]> Filter(IUnitOfWork unitOfWork, FilterEquipmentsQuery ascendingQuery)
    {
        return (await unitOfWork.ApplyQuery(ascendingQuery, CancellationToken.None))
            .ToBlockingEnumerable()
            .Select(t => t.Id)
            .ToArray();
    }

    private static IPostprocessComposer<CreateEquipmentDto> GenerateCreationRequest(
        Fixture fixture,
        int? roomId = null,
        int? schemaId = null)
    {
        IPostprocessComposer<CreateEquipmentDto> composer = fixture.Build<CreateEquipmentDto>();

        if (roomId != null)
        {
            composer = composer.With(t => t.RoomId, roomId);
        }

        if (schemaId != null)
        {
            composer = composer.With(t => t.SchemaId, schemaId);
        }

        return composer;
    }

    private async Task<FilterEquipmentTestContext> GenerateContext(
        Fixture fixture,
        string? room1Name = null,
        string? room2Name = null,
        string? room3Name = null,
        string? schema1Name = null,
        string? schema2Name = null,
        string? schema3Name = null)
    {
        var room1 = await mmrSdk.Rooms.CreateRoom(room1Name ?? fixture.Create<string>());
        var room2 = await mmrSdk.Rooms.CreateRoom(room2Name ?? fixture.Create<string>());
        var room3 = await mmrSdk.Rooms.CreateRoom(room3Name ?? fixture.Create<string>());

        var schema1 = await mmrSdk.Equipments.CreateEquipmentSchema(schema1Name);
        var schema2 = await mmrSdk.Equipments.CreateEquipmentSchema(schema2Name);
        var schema3 = await mmrSdk.Equipments.CreateEquipmentSchema(schema3Name);

        return new FilterEquipmentTestContext(
            new FilterEquipmentTestContextItem(room1, schema1),
            new FilterEquipmentTestContextItem(room2, schema2),
            new FilterEquipmentTestContextItem(room3, schema3));
    }

    private record FilterEquipmentTestContext(
        FilterEquipmentTestContextItem Equipment1,
        FilterEquipmentTestContextItem Equipment2,
        FilterEquipmentTestContextItem Equipment3);

    private record FilterEquipmentTestContextItem(RoomDto Room, EquipmentSchemaDto Schema);
}
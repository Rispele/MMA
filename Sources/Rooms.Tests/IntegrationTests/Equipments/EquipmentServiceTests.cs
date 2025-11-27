using AutoFixture;
using FluentAssertions;
using IntegrationTestInfrastructure;
using IntegrationTestInfrastructure.ContainerBasedTests;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Tests.Helpers.SDK;

namespace Rooms.Tests.IntegrationTests.Equipments;

[TestFixture]
public class EquipmentServiceTests : ContainerTestBase
{
    [Inject]
    private readonly IEquipmentService equipmentService = null!;

    [Inject]
    private readonly MmrSdk mmrSdk = null!;

    [Test]
    public async Task CreateEquipment_ShouldBeCreated()
    {
        var fixture = new Fixture();

        var room = await mmrSdk.Rooms.CreateRoom(Guid.NewGuid().ToString());
        var equipmentType = await mmrSdk.Equipments.CreateEquipmentType();
        var equipmentSchema = await mmrSdk.Equipments.CreateEquipmentSchema(equipmentType.Id);

        var createRequest = fixture
            .Build<CreateEquipmentDto>()
            .With(create => create.RoomId, room.Id)
            .With(create => create.SchemaId, equipmentSchema.Id)
            .Create();

        var created = await equipmentService.CreateEquipment(createRequest, CancellationToken.None);

        created.RoomId.Should().Be(room.Id);
        created.Schema.Should().BeEquivalentTo(equipmentSchema);
        created.Comment.Should().Be(createRequest.Comment);
        created.InventoryNumber.Should().Be(createRequest.InventoryNumber);
        created.SerialNumber.Should().Be(createRequest.SerialNumber);
        created.NetworkEquipmentIp.Should().Be(createRequest.NetworkEquipmentIp);
        created.Status.Should().Be(createRequest.Status);
    }

    [Test]
    public async Task PatchEquipment_ShouldBePatched()
    {
        var fixture = new Fixture();

        var room = await mmrSdk.Rooms.CreateRoom(Guid.NewGuid().ToString());
        var createdEquipment = await mmrSdk.Equipments.CreateEquipment(room.Id);

        var patch = fixture
            .Build<PatchEquipmentDto>()
            .With(t => t.RoomId, room.Id)
            .With(t => t.SchemaId, createdEquipment.Schema.Id)
            .Create();
        var patched = await equipmentService.PatchEquipment(createdEquipment.Id, patch, CancellationToken.None);

        patched.Should().NotBeEquivalentTo(createdEquipment);

        patched.RoomId.Should().Be(room.Id);
        patched.Schema.Should().BeEquivalentTo(createdEquipment.Schema);
        patched.Comment.Should().Be(patch.Comment);
        patched.InventoryNumber.Should().Be(patch.InventoryNumber);
        patched.SerialNumber.Should().Be(patch.SerialNumber);
        patched.NetworkEquipmentIp.Should().Be(patch.NetworkEquipmentIp);
        patched.Status.Should().Be(patch.Status);
    }

    [Test]
    public async Task GetEquipment_ShouldReturnCorrectly()
    {
        var room = await mmrSdk.Rooms.CreateRoom(Guid.NewGuid().ToString());
        var expected = await mmrSdk.Equipments.CreateEquipment(room.Id);

        var actual = await equipmentService.GetEquipmentById(expected.Id, CancellationToken.None);

        actual.Should().BeEquivalentTo(expected);
    }

    // [TestCaseSource(nameof(FilterEquipmentsTestCaseSource))]
    // public async Task FilterEquipment_ShouldReturnCorrectly(
    //     Func<Fixture, FilterEquipmentTestContext, CreateEquipmentDto> equipment1,
    //     Func<Fixture, FilterEquipmentTestContext, CreateEquipmentDto> equipment2,
    //     Func<Fixture, FilterEquipmentTestContext, CreateEquipmentDto> equipment3,
    //     Func<FilterEquipmentTestContext, SortDirectionDto, EquipmentsFilterDto> filter)
    // {
    //     var fixture = new Fixture();
    //
    //     var room1 = await mmrSdk.Rooms.CreateRoom(fixture.Create<string>());
    //     var room2 = await mmrSdk.Rooms.CreateRoom(fixture.Create<string>());
    //     var room3 = await mmrSdk.Rooms.CreateRoom(fixture.Create<string>());
    //
    //     var schema1 = await mmrSdk.Equipments.CreateEquipmentSchema();
    //     var schema2 = await mmrSdk.Equipments.CreateEquipmentSchema();
    //     var schema3 = await mmrSdk.Equipments.CreateEquipmentSchema();
    //
    //     var testContext = new FilterEquipmentTestContext(room1, room2, room3, schema1, schema2, schema3);
    //     
    //     var expected1 = await mmrSdk.Equipments.CreateEquipment(equipment1(fixture, testContext));
    //     var expected2 = await mmrSdk.Equipments.CreateEquipment(equipment2(fixture, testContext));
    //     var expected3 = await mmrSdk.Equipments.CreateEquipment(equipment3(fixture, testContext));
    //
    //     var actualAscending = await equipmentService.FilterEquipments(
    //         new GetEquipmentsDto(BatchNumber: 0, BatchSize: 1000, AfterEquipmentId: -1, filter(testContext, SortDirectionDto.Ascending)),
    //         CancellationToken.None);
    //     var actualDescending = await equipmentService.FilterEquipments(
    //         new GetEquipmentsDto(BatchNumber: 0, BatchSize: 1000, AfterEquipmentId: -1, filter(testContext, SortDirectionDto.Ascending)),
    //         CancellationToken.None);
    //
    //     actualAscending.Should().BeEquivalentTo(expected);
    // }
    //
    // private static IEnumerable<TestCaseData> FilterEquipmentsTestCaseSource()
    // {
    //     yield return CreateTestCase(
    //         equipment1: (composer, context) => composer
    //             .With(create => create.RoomId, context.Room1.Id)
    //             .With(create => create.SchemaId, context.Schema1.Id)
    //             .Create(),
    //         equipment2: (composer, context) => composer
    //             .With(create => create.RoomId, context.Room2.Id)
    //             .With(create => create.SchemaId, context.Schema2.Id)
    //             .Create(),
    //         equipment3: (composer, context) => composer
    //             .With(create => create.RoomId, context.Room1.Id)
    //             .With(create => create.SchemaId, context.Schema3.Id)
    //             .Create(),
    //         filter: (context, direction) => new EquipmentsFilterDto { Rooms = new FilterMultiParameterDto<int>([context.Room1.Id], direction) });
    //
    //     yield return CreateTestCase(
    //         equipment1: (composer, context) => composer
    //             .With(create => create.RoomId, context.Room1.Id)
    //             .With(create => create.SchemaId, context.Schema1.Id)
    //             .Create(),
    //         equipment2: (composer, context) => composer
    //             .With(create => create.RoomId, context.Room2.Id)
    //             .With(create => create.SchemaId, context.Schema2.Id)
    //             .Create(),
    //         equipment3: (composer, context) => composer
    //             .With(create => create.RoomId, context.Room3.Id)
    //             .With(create => create.SchemaId, context.Schema1.Id)
    //             .Create(),
    //         filter: (context, direction) => new EquipmentsFilterDto { Schemas = new FilterMultiParameterDto<int>([context.Schema1.Id], direction) });
    //
    //     yield break;
    //
    //     TestCaseData CreateTestCase(
    //         Func<ICustomizationComposer<CreateEquipmentDto>, FilterEquipmentTestContext, CreateEquipmentDto> equipment1,
    //         Func<ICustomizationComposer<CreateEquipmentDto>, FilterEquipmentTestContext, CreateEquipmentDto> equipment2,
    //         Func<ICustomizationComposer<CreateEquipmentDto>, FilterEquipmentTestContext, CreateEquipmentDto> equipment3,
    //         Func<FilterEquipmentTestContext, SortDirectionDto, EquipmentsFilterDto> filter)
    //     {
    //         return CreateFixtureTestCase(
    //             (fixture, context) => equipment1(fixture.Build<CreateEquipmentDto>(), context),
    //             (fixture, context) => equipment2(fixture.Build<CreateEquipmentDto>(), context),
    //             (fixture, context) => equipment3(fixture.Build<CreateEquipmentDto>(), context),
    //             filter);
    //     }
    //
    //     TestCaseData CreateFixtureTestCase(
    //         Func<Fixture, FilterEquipmentTestContext, CreateEquipmentDto> equipment1,
    //         Func<Fixture, FilterEquipmentTestContext, CreateEquipmentDto> equipment2,
    //         Func<Fixture, FilterEquipmentTestContext, CreateEquipmentDto> equipment3,
    //         Func<FilterEquipmentTestContext, SortDirectionDto, EquipmentsFilterDto> filter)
    //     {
    //         return new TestCaseData(
    //             equipment1,
    //             equipment2,
    //             equipment3,
    //             filter);
    //     }
    // }
    //
    // public record FilterEquipmentTestContext(
    //     RoomDto Room1,
    //     RoomDto Room2,
    //     RoomDto Room3,
    //     EquipmentSchemaDto Schema1,
    //     EquipmentSchemaDto Schema2,
    //     EquipmentSchemaDto Schema3);
}
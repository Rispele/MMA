using AutoFixture;
using Commons.Tests.Helpers.SDK;
using FluentAssertions;
using Commons.Tests.Integration.Infrastructure;
using Commons.Tests.Integration.Infrastructure.ContainerBasedTests;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Services.Equipments;

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
}
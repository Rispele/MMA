using Commons;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.Equipments;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentSchemas;
using Rooms.Core.Interfaces.Dtos.Equipment.Requests.EquipmentTypes;
using Rooms.Core.Interfaces.Dtos.Equipment.Responses;
using Rooms.Core.Interfaces.Dtos.Files;
using Rooms.Core.Interfaces.Dtos.OperatorDepartments;
using Rooms.Core.Interfaces.Dtos.Room;
using Rooms.Core.Interfaces.Dtos.Room.Requests;
using Rooms.Core.Interfaces.Dtos.Room.Responses;
using Rooms.Core.Interfaces.Services.Equipments;
using Rooms.Core.Interfaces.Services.OperatorDepartments;
using Rooms.Core.Interfaces.Services.Rooms;
using Rooms.Core.Services.Spreadsheets;
using Rooms.Core.Services.Spreadsheets.Abstractions;
using Rooms.Core.Services.Spreadsheets.ExportModels;
using Rooms.Core.Services.Spreadsheets.Specifications;

namespace Rooms.Tests.UnitTests.Spreadsheets;

[TestFixture]
public class SpreadsheetServiceTests
{
    [Test]
    public async Task ExportRoomRegistry_ShouldSuccessfullyExport()
    {
        using var spreadsheetServiceContext = SetupSpreadsheetService();
        var spreadsheetService = spreadsheetServiceContext.SpreadsheetService;

        var room = RoomTestHelper.CreateRoomDto(operatorDepartmentId: 1);
        var operatorDepartment = new OperatorDepartmentDto
        {
            Contacts = "123",
            Id = 1,
            Name = "Name",
            Operators = new Dictionary<string, string>
            {
                ["a"] = "b"
            },
            Rooms =
            [
                new OperatorDepartmentRoomInfoDto(1, new ScheduleAddressDto("1", "2"))
            ]
        };

        spreadsheetServiceContext.RoomService.Setup(t => t
                .FilterRooms(
                    It.Is<GetRoomsRequestDto>(roomRequest => roomRequest.AfterId == -1
                                                             && roomRequest.BatchNumber == 0
                                                             && roomRequest.BatchSize == SpreadsheetService.ExportLimit
                                                             && roomRequest.Filter == null),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new RoomsResponseDto([room], Count: 1, LastRoomId: 1));

        spreadsheetServiceContext.OperatorDepartmentService.Setup(t => t
                .GetOperatorDepartmentsById(
                    It.Is(new[] { 1 }, new ArrayStructuralComparer<int>()),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync([operatorDepartment]);

        var exportModel = new RoomRegistrySpreadsheetExportDto
        {
            Room = room,
            OperatorDepartment = operatorDepartment
        };

        spreadsheetServiceContext.SetupExporter<
            RoomRegistrySpreadsheetSpecification,
            RoomRegistrySpreadsheetSpecification,
            RoomRegistrySpreadsheetExportDto>([exportModel]);

        var action = () => spreadsheetService.ExportRoomRegistry(new MemoryStream(), CancellationToken.None);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task ExportEquipmentRegistry_ShouldSuccessfullyExport()
    {
        using var spreadsheetServiceContext = SetupSpreadsheetService();
        var spreadsheetService = spreadsheetServiceContext.SpreadsheetService;

        var equipment = EquipmentsTestHelper.CreateEquipmentDto();
        var room = RoomTestHelper.CreateRoomDto(equipment.RoomId);

        spreadsheetServiceContext.EquipmentService
            .Setup(t => t.FilterEquipments(
                new GetEquipmentsDto(0, SpreadsheetService.ExportLimit, -1, null),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EquipmentsResponseDto([equipment], 1, 1));

        spreadsheetServiceContext.RoomService
            .Setup(t => t.FindRoomByIds(
                It.Is(new [] {room.Id}, new ArrayStructuralComparer<int>()),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([room]);

        var exportModel = new EquipmentRegistrySpreadsheetExportDto
        {
            RoomName = room.Name,
            TypeName = equipment.Schema.Type.Name,
            SchemaName = equipment.Schema.Name,
            Comment = equipment.Comment,
            InventoryNumber = equipment.InventoryNumber,
            SerialNumber = equipment.SerialNumber,
            Status = equipment.Status.ToString(),
        };

        spreadsheetServiceContext.SetupExporter<
            EquipmentRegistrySpreadsheetSpecification,
            EquipmentRegistrySpreadsheetSpecification,
            EquipmentRegistrySpreadsheetExportDto>([exportModel]);

        var action = () => spreadsheetService.ExportEquipmentRegistry(new MemoryStream(), CancellationToken.None);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task ExportEquipmentSchemaRegistry_ShouldSuccessfullyExport()
    {
        using var spreadsheetServiceContext = SetupSpreadsheetService();
        var spreadsheetService = spreadsheetServiceContext.SpreadsheetService;

        var equipmentSchema = EquipmentsTestHelper.CreateEquipmentSchemaDto();
        spreadsheetServiceContext.EquipmentSchemaService
            .Setup(t => t.FilterEquipmentSchemas(
                new GetEquipmentSchemasDto(0, SpreadsheetService.ExportLimit, -1, null),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EquipmentSchemasResponseDto([equipmentSchema], 1, 1));

        var exportModel = new EquipmentSchemaRegistrySpreadsheetExportDto
        {
            Name = equipmentSchema.Name,
            TypeName = equipmentSchema.Type.Name,
            Parameters = equipmentSchema.ParameterValues.Select(pair => $"{pair.Key} = {pair.Value}").JoinStrings(Environment.NewLine),
        };

        spreadsheetServiceContext.SetupExporter<
            EquipmentSchemaRegistrySpreadsheetSpecification,
            EquipmentSchemaRegistrySpreadsheetSpecification,
            EquipmentSchemaRegistrySpreadsheetExportDto>([exportModel]);

        var action = () => spreadsheetService.ExportEquipmentSchemaRegistry(new MemoryStream(), CancellationToken.None);

        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task ExportEquipmentTypeRegistry_ShouldSuccessfullyExport()
    {
        using var spreadsheetServiceContext = SetupSpreadsheetService();
        var spreadsheetService = spreadsheetServiceContext.SpreadsheetService;

        var equipmentType = EquipmentsTestHelper.CreateEquipmentTypeDto();
        spreadsheetServiceContext.EquipmentTypeService
            .Setup(t => t.FilterEquipmentTypes(
                new GetEquipmentTypesDto(0, SpreadsheetService.ExportLimit, -1, null),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EquipmentTypesResponseDto([equipmentType], 1, 1));

        var exportModel = new EquipmentTypeRegistrySpreadsheetExportDto
        {
            Name = equipmentType.Name,
        };

        spreadsheetServiceContext.SetupExporter<
            EquipmentTypeRegistrySpreadsheetSpecification,
            EquipmentTypeRegistrySpreadsheetSpecification,
            EquipmentTypeRegistrySpreadsheetExportDto>([exportModel]);

        var action = () => spreadsheetService.ExportEquipmentTypeRegistry(new MemoryStream(), CancellationToken.None);

        await action.Should().NotThrowAsync();
    }

    private static SpreadsheetServiceContext SetupSpreadsheetService()
    {
        var mockRepository = new MockRepository(MockBehavior.Strict);

        var exporter = mockRepository.Create<ISpreadsheetExporter>();
        var equipmentTypeService = mockRepository.Create<IEquipmentTypeService>();
        var equipmentSchemaService = mockRepository.Create<IEquipmentSchemaService>();
        var equipmentService = mockRepository.Create<IEquipmentService>();
        var roomService = mockRepository.Create<IRoomService>();
        var operatorDepartmentService = mockRepository.Create<IOperatorDepartmentService>();

        var service = new SpreadsheetService(
            exporter.Object,
            equipmentTypeService.Object,
            equipmentSchemaService.Object,
            equipmentService.Object,
            roomService.Object,
            operatorDepartmentService.Object);

        return new SpreadsheetServiceContext(
            mockRepository,
            exporter,
            equipmentTypeService,
            equipmentSchemaService,
            equipmentService,
            roomService,
            operatorDepartmentService,
            service);
    }

    private record SpreadsheetServiceContext(
        MockRepository MockRepository,
        Mock<ISpreadsheetExporter> Exporter,
        Mock<IEquipmentTypeService> EquipmentTypeService,
        Mock<IEquipmentSchemaService> EquipmentSchemaService,
        Mock<IEquipmentService> EquipmentService,
        Mock<IRoomService> RoomService,
        Mock<IOperatorDepartmentService> OperatorDepartmentService,
        SpreadsheetService SpreadsheetService) : IDisposable
    {
        public void Dispose()
        {
            MockRepository.VerifyAll();
        }

        public void SetupExporter<TExporterSpecification, TWriterSpecification, TData>(TData[] data)
            where TExporterSpecification : struct, ISpreadsheetExporterSpecification
            where TWriterSpecification : struct, ISpreadsheetWriterSpecification<TData>
        {
            Exporter
                .Setup(exporter => exporter
                    .Export<TExporterSpecification, TWriterSpecification, TData>(
                        It.Is(data, new ArrayStructuralComparer<TData>()),
                        It.IsAny<Stream>(),
                        It.IsAny<CancellationToken>()))
                .Returns(new FileExportDto
                {
                    FileName = "123",
                    ContentType = "123",
                    Flush = () => {}
                });
        }
    }
}
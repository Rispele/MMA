using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Rooms.Core.Dtos.Files;
using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.Requests.Equipments;
using Rooms.Core.Dtos.Requests.Rooms;
using Rooms.Core.Dtos.Responses;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Services.Interfaces;
using Rooms.Core.Spreadsheets;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExportModels;
using Rooms.Core.Spreadsheets.Specifications;

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
                    It.Is<GetRoomsRequestDto>(roomRequest => roomRequest.AfterRoomId == -1
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

        var action = () => spreadsheetService.ExportRoomRegistry(CancellationToken.None);

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

        var action = () => spreadsheetService.ExportEquipmentRegistry(CancellationToken.None);

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
                        It.IsAny<CancellationToken>()))
                .Returns(new FileExportDto
                {
                    FileName = "123",
                    Content = new MemoryStream(),
                    ContentType = "123"
                });
        }
    }
}
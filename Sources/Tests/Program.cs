using FluentAssertions;
using Rooms.Domain.Models.Equipment;
using WebApi.Models.Equipment;
using WebApi.Models.OperatorRoom;
using WebApi.Models.Requests.Equipments;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Requests.OperatorRooms;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;

namespace Tests;

public class Program
{
    private const string BaseUrl = "http://localhost:5049";
    private readonly WebApiHttpClient _httpClient = new(new HttpClient(), BaseUrl);

    [SetUp]
    public void Setup()
    {
    }

    [TearDown]
    public void Teardown()
    {
    }

    [Test]
    public async Task CreateEquipmentTypeTest()
    {
        // Arrange
        var inputType = new CreateEquipmentTypeModel
        {
            Name = "Проектор",
            Parameters = [new EquipmentParameterDescriptorModel { Name = "Разрешение", Required = true }]
        };
        var expectedType = new EquipmentTypeModel
        {
            Name = "Проектор",
            Parameters = [new EquipmentParameterDescriptorModel { Name = "Разрешение", Required = true }]
        };

        // Act
        var actualType = await _httpClient.CreateEquipmentType(inputType);

        // Assert
        actualType.Should().BeEquivalentTo(expectedType, config: opt => opt.Excluding(x => x.Id));
    }

    [Test]
    public async Task CreateEquipmentSchemaTest()
    {
        // Arrange
        var type = new CreateEquipmentTypeModel
        {
            Name = "Проектор",
            Parameters = [new EquipmentParameterDescriptorModel { Name = "Разрешение", Required = true }]
        };
        var actualType = await _httpClient.CreateEquipmentType(type);

        var inputSchema = new CreateEquipmentSchemaModel
        {
            Name = "Первое оборудование",
            EquipmentTypeId = actualType.Id,
            ParameterValues = new Dictionary<string, string> { { "Разрешение", "1920x1080" } },
            EquipmentIds = []
        };
        var expectedSchema = new EquipmentSchemaModel
        {
            Name = "Первое оборудование",
            TypeId = actualType.Id,
            Type = new EquipmentTypeModel
            {
                Id = actualType.Id,
                Name = "Проектор",
                Parameters = [new EquipmentParameterDescriptorModel { Name = "Разрешение", Required = true }]
            },
            ParameterValues = new Dictionary<string, string> { { "Разрешение", "1920x1080" } }
        };

        // Act
        var actualSchema = await _httpClient.CreateEquipmentSchema(inputSchema);

        // Assert
        actualSchema.Should().BeEquivalentTo(expectedSchema, config: opt => opt.Excluding(x => x.Id));
    }

    [Test]
    public async Task CreateRoomTest()
    {
        // Arrange
        var inputRoom = new CreateRoomModel
        {
            Name = "ПерваяАудитория",
            Description = "Описание",
            Type = RoomTypeModel.Computer,
            Layout = RoomLayoutModel.Unspecified,
            Seats = 15,
            ComputerSeats = 10,
            PdfRoomSchemeFile = null,
            PhotoFile = null,
            NetType = RoomNetTypeModel.Wired,
            HasConditioning = true,
            Owner = null,
            RoomStatus = RoomStatusModel.Ready,
            Comment = null,
            FixDeadline = null,
            AllowBooking = true
        };
        var expectedRoom = new RoomModel
        {
            Id = 1,
            Name = "ПерваяАудитория",
            Description = "Описание",
            // ScheduleAddress = new ScheduleAddressModel
            // {
            //     Address = "Мира 19",
            //     RoomNumber = "150"
            // },
            Parameters = new RoomParametersModel
            {
                Type = RoomTypeModel.Computer,
                Layout = RoomLayoutModel.Unspecified,
                NetType = RoomNetTypeModel.Wired,
                Seats = 15,
                ComputerSeats = 10,
                HasConditioning = true
            },
            Attachments = new RoomAttachmentsModel(PdfRoomScheme: null, Photo: null),
            Owner = null,
            OperatorDepartment = null,
            FixStatus = new RoomFixStatusModel(RoomStatusModel.Ready, FixDeadline: null, Comment: null),
            AllowBooking = true,
            Equipments = []
        };

        // Act
        var actualRoom = await _httpClient.CreateRoom(inputRoom);

        // Assert
        actualRoom.Should().BeEquivalentTo(expectedRoom, config: opt => opt.Excluding(x => x.Id));
    }

    [Test]
    public async Task ModelsRelationMatchingTest()
    {
        // Arrange
        var createEquipmentTypeModel = new CreateEquipmentTypeModel
        {
            Name = "Проектор",
            Parameters = [new EquipmentParameterDescriptorModel { Name = "Разрешение", Required = true }]
        };
        var equipmentType = await _httpClient.CreateEquipmentType(createEquipmentTypeModel);

        var createEquipmentSchemaModel = new CreateEquipmentSchemaModel
        {
            Name = "Первое оборудование",
            EquipmentTypeId = equipmentType.Id,
            ParameterValues = new Dictionary<string, string>() { { "Разрешение", "1920x1080" } },
            EquipmentIds = []
        };
        var equipmentSchema = await _httpClient.CreateEquipmentSchema(createEquipmentSchemaModel);

        var createRoomModel = new CreateRoomModel
        {
            Name = "ВтораяАудитория",
            Description = "Описание",
            Type = RoomTypeModel.Computer,
            Layout = RoomLayoutModel.Unspecified,
            Seats = 15,
            ComputerSeats = 10,
            PdfRoomSchemeFile = null,
            PhotoFile = null,
            NetType = RoomNetTypeModel.Wired,
            HasConditioning = true,
            Owner = null,
            RoomStatus = RoomStatusModel.Ready,
            Comment = null,
            FixDeadline = null,
            AllowBooking = true,
        };
        var room = await _httpClient.CreateRoom(createRoomModel);

        var createEquipmentModel = new CreateEquipmentModel
        {
            RoomId = room.Id,
            SchemaId = equipmentSchema.Id,
            InventoryNumber = "1234",
            SerialNumber = "5678",
            NetworkEquipmentIp = "127.0.0.1",
            Comment = null,
            Status = EquipmentStatus.Ok,
        };
        var equipment = await _httpClient.CreateEquipment(createEquipmentModel);

        var expectedRoom = new RoomModel
        {
            Id = 1,
            Name = "ВтораяАудитория",
            Description = "Описание",
            // ScheduleAddress = new ScheduleAddressModel
            // {
            //     Address = "Мира 19",
            //     RoomNumber = "150"
            // },
            Parameters = new RoomParametersModel
            {
                Type = RoomTypeModel.Computer,
                Layout = RoomLayoutModel.Unspecified,
                NetType = RoomNetTypeModel.Wired,
                Seats = 15,
                ComputerSeats = 10,
                HasConditioning = true
            },
            Attachments = new RoomAttachmentsModel(null, null),
            Owner = null,
            OperatorDepartment = null,
            FixStatus = new RoomFixStatusModel(RoomStatusModel.Ready, null, null),
            AllowBooking = true,
            Equipments =
            [
                new EquipmentModel
                {
                    Id = equipment.Id,
                    RoomId = room.Id,
                    SchemaId = equipmentSchema.Id,
                    Schema = equipmentSchema,
                    InventoryNumber = "1234",
                    SerialNumber = "5678",
                    NetworkEquipmentIp = "127.0.0.1",
                    Comment = null,
                    Status = EquipmentStatus.Ok,
                }
            ],
        };

        // Act
        var actualRoom = await _httpClient.GetRoomById(room.Id);

        // Assert
        actualRoom.Should().BeEquivalentTo(expectedRoom, opt => opt.Excluding(x => x.Id));
    }

    [Test]
    public async Task CreateOperatorRoomTest()
    {
        // Arrange
        var createRoomModel = new CreateRoomModel
        {
            Name = "ТретьяАудитория",
            Description = "Описание",
            Type = RoomTypeModel.Computer,
            Layout = RoomLayoutModel.Unspecified,
            Seats = 15,
            ComputerSeats = 10,
            PdfRoomSchemeFile = null,
            PhotoFile = null,
            NetType = RoomNetTypeModel.Wired,
            HasConditioning = true,
            Owner = null,
            RoomStatus = RoomStatusModel.Ready,
            Comment = null,
            FixDeadline = null,
            AllowBooking = true,
        };
        var room = await _httpClient.CreateRoom(createRoomModel);

        var inputOperatorRoom = new CreateOperatorRoomModel
        {
            Name = "Операторская",
            RoomIds = [room.Id],
            Operators = new Dictionary<string, string> { { "1", "Иван" } },
            Contacts = "123",
        };
        var expectedOperatorRoom = new OperatorRoomModel
        {
            Name = "Операторская",
            Rooms = new Dictionary<int, string>() { { 1, "ТретьяАудитория" } },
            Operators = new Dictionary<string, string>() { { "1", "Иван" } },
            Contacts = "123",
        };

        // Act
        var actualOperatorRoom = await _httpClient.CreateOperatorRoom(inputOperatorRoom);

        // Assert
        actualOperatorRoom.Should().BeEquivalentTo(expectedOperatorRoom, opt => opt.Excluding(x => x.Id));
    }
}
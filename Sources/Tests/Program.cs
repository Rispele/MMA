using FluentAssertions;
using WebApi.Models.Equipment;
using WebApi.Models.Requests.EquipmentSchemas;
using WebApi.Models.Requests.EquipmentTypes;
using WebApi.Models.Requests.Rooms;
using WebApi.Models.Room;

namespace Tests;

public class Program
{
    private readonly WebApiHttpClient _httpClient = new(new HttpClient(), BaseUrl);
    private const string BaseUrl = "http://localhost:5049";

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
        actualType.Should().BeEquivalentTo(expectedType, opt => opt.Excluding(x => x.Id));
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
            ParameterValues = new Dictionary<string, string>() { { "Разрешение", "1920x1080" } },
            EquipmentIds = []
        };
        var expectedSchema = new EquipmentSchemaModel
        {
            EquipmentTypeId = actualType.Id,
            Name = "Первое оборудование",
            ParameterValues = new Dictionary<string, string>() { { "Разрешение", "1920x1080" } },
        };

        // Act
        var actualSchema = await _httpClient.CreateEquipmentSchema(inputSchema);

        // Assert
        actualSchema.Should().BeEquivalentTo(expectedSchema, opt => opt.Excluding(x => x.Id));
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
            AllowBooking = true,
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
            Attachments = new RoomAttachmentsModel(null, null),
            Owner = null,
            OperatorDepartment = null,
            FixStatus = new RoomFixStatusModel(RoomStatusModel.Ready, null, null),
            AllowBooking = true,
            Equipments = [],
        };

        // Act
        var actualRoom = await _httpClient.CreateRoom(inputRoom);

        // Assert
        actualRoom.Should().BeEquivalentTo(expectedRoom, opt => opt.Excluding(x => x.Id));
    }
}
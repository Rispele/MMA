using Commons;
using FluentAssertions;
using MathNet.Numerics.Random;
using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos.OperatorDepartments;
using Rooms.Core.Dtos.Room;
using Rooms.Core.Dtos.Room.Fix;
using Rooms.Core.Dtos.Room.Parameters;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExportModels;
using Rooms.Core.Spreadsheets.Specifications;
using Rooms.Infrastructure.Spreadsheets;
using Rooms.Tests.Helpers;

namespace Rooms.Tests.UnitTests.Spreadsheets;

[TestFixture]
public class SpreadsheetExporterTests
{
    [TestCaseSource(nameof(GetTestCases))]
    public void Export_ShouldSuccessfullyExport<TData>(
        ISpreadsheetExporterSpecification exporterSpecification,
        ISpreadsheetWriterSpecification<TData> writerSpecification,
        TData[] data,
        string expectedContentResourceName)
    {
        var exporter = new ExcelExporter();

        var result = exporter.Export(exporterSpecification, writerSpecification, data);

        result.FileName.Should().Be(exporterSpecification.FileName);

        var expectedContent = EmbeddedResourceProvider.GetEmbeddedResourceStream(expectedContentResourceName);
        SpreadsheetsAssertionHelper.AssertWorkbooks(
            exporterSpecification.SheetName,
            actualSpreadsheet: new XSSFWorkbook(result.Content),
            expectedSpreadsheet: new XSSFWorkbook(expectedContent));

        // For tests case creation
        // using var stream = File.OpenWrite(path: @"E:\Education\MMA\backend\Sources\Rooms.Tests\UnitTests\Spreadsheets\TestCases\" + expectedContentResourceName);
        // result.Content.CopyTo(stream);
        // stream.Close();
    }

    public static IEnumerable<TestCaseData> GetTestCases()
    {
        var random = new Random(Seed: 0);

        yield return new TestCaseData(
            new EquipmentRegistrySpreadsheetSpecification(),
            new EquipmentRegistrySpreadsheetSpecification(),
            new[]
            {
                GenerateEquipmentRegistryExcelExportDto(random),
                GenerateEquipmentRegistryExcelExportDto(random),
                GenerateEquipmentRegistryExcelExportDto(random),
            },
            "equipments-verified.xlsx");

        yield return new TestCaseData(
            new EquipmentSchemaRegistrySpreadsheetSpecification(),
            new EquipmentSchemaRegistrySpreadsheetSpecification(),
            new[]
            {
                GenerateEquipmentSchemaRegistryExcelExportDto(random),
                GenerateEquipmentSchemaRegistryExcelExportDto(random),
                GenerateEquipmentSchemaRegistryExcelExportDto(random),
            },
            "equipment-schemas-verified.xlsx");

        yield return new TestCaseData(
            new EquipmentTypeRegistrySpreadsheetSpecification(),
            new EquipmentTypeRegistrySpreadsheetSpecification(),
            new[]
            {
                GenerateEquipmentTypeRegistryExcelExportDto(random),
                GenerateEquipmentTypeRegistryExcelExportDto(random),
                GenerateEquipmentTypeRegistryExcelExportDto(random)
            },
            "equipment-types-verified.xlsx");

        yield return new TestCaseData(
            new RoomRegistrySpreadsheetSpecification(),
            new RoomRegistrySpreadsheetSpecification(),
            new[]
            {
                GenerateRoomRegistryExcelExportDto(random),
                GenerateRoomRegistryExcelExportDto(random),
                GenerateRoomRegistryExcelExportDto(random)
            },
            "rooms-verified.xlsx");
    }

    private static EquipmentRegistrySpreadsheetExportDto GenerateEquipmentRegistryExcelExportDto(Random random)
    {
        return new EquipmentRegistrySpreadsheetExportDto
        {
            RoomName = random.NextString(),
            SchemaName = random.NextString(),
            TypeName = random.NextString(),
            Comment = random.NextString(),
            InventoryNumber = random.NextString(),
            SerialNumber = random.NextString(),
            Status = random.NextString(),
        };
    }

    private static EquipmentSchemaRegistrySpreadsheetExportDto GenerateEquipmentSchemaRegistryExcelExportDto(Random random)
    {
        return new EquipmentSchemaRegistrySpreadsheetExportDto
        {
            Name = random.NextString(),
            TypeName = random.NextString(),
            Parameters = random.NextString() + Environment.NewLine + random.NextString()
        };
    }

    private static EquipmentTypeRegistrySpreadsheetExportDto GenerateEquipmentTypeRegistryExcelExportDto(Random random)
    {
        return new EquipmentTypeRegistrySpreadsheetExportDto
        {
            Name = random.NextString()
        };
    }

    private static RoomRegistrySpreadsheetExportDto GenerateRoomRegistryExcelExportDto(Random random)
    {
        return new RoomRegistrySpreadsheetExportDto
        {
            Room = new RoomDto
            {
                AllowBooking = random.NextBoolean(),
                Attachments = new RoomAttachmentsDto(null, null),
                Description = random.NextString(),
                Equipments = [],
                FixInfo = new RoomFixStatusDto((RoomStatusDto)random.Next(4), new DateTime(random.NextInt64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks)), random.NextString()),
                Id = random.Next(),
                Name = random.NextString(),
                OperatorDepartmentId = 0,
                Owner = random.NextString(),
                Parameters = new RoomParametersDto(
                    (RoomTypeDto)random.Next(5),
                    (RoomLayoutDto)random.Next(3),
                    (RoomNetTypeDto)random.Next(5),
                    random.Next(),
                    random.Next(),
                    random.NextBoolean())
            },
            OperatorDepartment = new OperatorDepartmentDto
            {
                Contacts = random.NextString(),
                Id = random.Next(),
                Name = random.NextString(),
                Operators = new Dictionary<string, string>
                {
                    [random.NextString()] = random.NextString(),
                    [random.NextString()] = random.NextString(),
                    [random.NextString()] = random.NextString(),
                },
                Rooms = []
            }
        };
    }
}
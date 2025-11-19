using Commons;
using FluentAssertions;
using NPOI.XSSF.UserModel;
using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Spreadsheets.Abstractions;
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

        // resultContent.Should().BeEquivalentTo(expectedContent);

        // For tests case creation
        // await using var stream = File.OpenWrite(path: @"E:\Education\MMA\backend\Sources\Rooms.Tests\UnitTests\Spreadsheets\TestCases\" + expectedContentResourceName);
        // await result.Content.CopyToAsync(stream);
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
    }

    private static EquipmentRegistryExcelExportDto GenerateEquipmentRegistryExcelExportDto(Random random)
    {
        return new EquipmentRegistryExcelExportDto
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

    private static EquipmentSchemaRegistryExcelExportDto GenerateEquipmentSchemaRegistryExcelExportDto(Random random)
    {
        return new EquipmentSchemaRegistryExcelExportDto
        {
            Name = random.NextString(),
            TypeName = random.NextString(),
            Parameters = random.NextString() + Environment.NewLine + random.NextString()
        };
    }

    private static EquipmentTypeRegistryExcelExportDto GenerateEquipmentTypeRegistryExcelExportDto(Random random)
    {
        return new EquipmentTypeRegistryExcelExportDto()
        {
            Name = random.NextString()
        };
    }
}
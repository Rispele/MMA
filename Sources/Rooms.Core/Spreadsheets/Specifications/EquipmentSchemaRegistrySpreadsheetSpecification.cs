using Rooms.Core.Dtos.Equipment;
using Rooms.Core.Spreadsheets.Abstractions;
using Rooms.Core.Spreadsheets.ExcelValueTypes;

namespace Rooms.Core.Spreadsheets.Specifications;

public struct EquipmentSchemaRegistrySpreadsheetSpecification
    : ISpreadsheetExporterSpecification, ISpreadsheetWriterSpecification<EquipmentSchemaRegistryExcelExportDto>
{
    private static readonly IReadOnlyList<ColumnSpecification<EquipmentSchemaRegistryExcelExportDto>> Specifications =
    [
        new(Name: "Наименование", data => new StringSpreadsheetValueType(data.Name)),
        new(Name: "Тип оборудования", data => new StringSpreadsheetValueType(data.TypeName)),
        new(Name: "Параметры", data => new StringSpreadsheetValueType(data.Parameters)),
    ];

    public string SheetName => "Модели оборудования";
    public string FileName => "Модели оборудования.xlsx";

    public IReadOnlyList<ColumnSpecification<EquipmentSchemaRegistryExcelExportDto>> ColumnSpecifications => Specifications;
}
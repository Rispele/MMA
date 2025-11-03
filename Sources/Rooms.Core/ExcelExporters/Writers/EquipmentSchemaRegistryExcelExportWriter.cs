using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.ExcelValueTypes;

namespace Rooms.Core.ExcelExporters.Writers;

public class EquipmentSchemaRegistryExcelExportWriter : ExcelWriterBase<EquipmentSchemaRegistryExcelExportDto>
{
	public override List<string> ColumnNames { get; } =
	[
		"Наименование",
		"Тип оборудования",
		"Параметры",
	];

	protected override IEnumerable<ColumnCellData> MapCellValues(EquipmentSchemaRegistryExcelExportDto dto)
	{
		return new List<ColumnCellData>()
		{
			new(0, new StringExcelValueType(dto.EquipmentName)),
			new(1, new StringExcelValueType(dto.EquipmentType)),
			new(2, new StringExcelValueType(dto.Parameters)),
		};
	}
}
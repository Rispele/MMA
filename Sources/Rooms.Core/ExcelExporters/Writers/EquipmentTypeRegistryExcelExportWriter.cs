using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.ExcelValueTypes;

namespace Rooms.Core.ExcelExporters.Writers;

public class EquipmentTypeRegistryExcelExportWriter : ExcelWriterBase<EquipmentTypeRegistryExcelExportDto>
{
	public override List<string> ColumnNames { get; } =
	[
		"Наименование",
	];

	protected override IEnumerable<ColumnCellData> MapCellValues(EquipmentTypeRegistryExcelExportDto dto)
	{
		return new List<ColumnCellData>()
		{
			new(0, new StringExcelValueType(dto.Name)),
		};
	}
}
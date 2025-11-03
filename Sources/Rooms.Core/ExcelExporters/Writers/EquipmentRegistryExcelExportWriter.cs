using Rooms.Core.Dtos.Equipment;
using Rooms.Core.ExcelExporters.ExcelValueTypes;

namespace Rooms.Core.ExcelExporters.Writers;

public class EquipmentRegistryExcelExportWriter : ExcelWriterBase<EquipmentRegistryExcelExportDto>
{
	public override List<string> ColumnNames { get; } =
	[
		"Аудитория",
		"Тип оборудования",
		"Модель оборудования",
		"Инвентарный номер",
		"Серийный номер",
		"Комментарий",
		"Статус",
	];

	protected override IEnumerable<ColumnCellData> MapCellValues(EquipmentRegistryExcelExportDto dto)
	{
		return new List<ColumnCellData>()
		{
			new(0, new StringExcelValueType(dto.RoomName)),
			new(1, new StringExcelValueType(dto.EquipmentType)),
			new(2, new StringExcelValueType(dto.EquipmentSchemaName)),
			new(3, new StringExcelValueType(dto.InventoryNumber)),
			new(4, new StringExcelValueType(dto.SerialNumber)),
			new(5, new StringExcelValueType(dto.Comment)),
			new(6, new StringExcelValueType(dto.Status)),
		};
	}
}
using Rooms.Core.Dtos.Room;
using Rooms.Core.ExcelExporters.ExcelValueTypes;

namespace Rooms.Core.ExcelExporters.Writers;

public class RoomRegistryExcelExportWriter : ExcelWriterBase<RoomRegistryExcelExportDto>
{
	public override List<string> ColumnNames { get; } =
	[
		"Аудитория",
		"Тип",
		"Вмест.",
		"Вмест. с ПК",
		"Сеть",
		"Операторская",
		"Владелец",
		"Степень готовности",
	];

	protected override IEnumerable<ColumnCellData> MapCellValues(RoomRegistryExcelExportDto dto)
	{
		return new List<ColumnCellData>()
		{
			new(0, new StringExcelValueType(dto.Name)),
			new(1, new StringExcelValueType(dto.RoomType)),
			new(2, new StringExcelValueType(dto.Seats.ToString())),
			new(3, new StringExcelValueType(dto.ComputerSeats.ToString())),
			new(4, new StringExcelValueType(dto.NetworkEquipmentIp)),
			new(5, new StringExcelValueType(dto.OperatorDepartmentName)),
			new(6, new StringExcelValueType(dto.Owner)),
			new(7, new StringExcelValueType(dto.Status)),
		};
	}
}
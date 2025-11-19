using NPOI.SS.UserModel;

namespace Rooms.Core.Spreadsheets.ExcelValueTypes;

public class StringExcelValueType(string? value) : IExcelValueType
{
    private string? Value { get; set; } = value;

    public void WriteToExcel(IRow row, int colNumber)
    {
        row.CreateCell(colNumber).SetCellValue(Value);
    }
}
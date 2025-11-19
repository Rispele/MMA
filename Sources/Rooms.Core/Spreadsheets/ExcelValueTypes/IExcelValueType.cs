using NPOI.SS.UserModel;

namespace Rooms.Core.Spreadsheets.ExcelValueTypes;

public interface IExcelValueType
{
    void WriteToExcel(IRow row, int colNumber);
}
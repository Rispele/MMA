using NPOI.SS.UserModel;

namespace Rooms.Core.Spreadsheets.ExcelValueTypes;

public interface ISpreadsheetValueType
{
    void WriteToExcel(IRow row, int colNumber);
}
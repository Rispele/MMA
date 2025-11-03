using NPOI.SS.UserModel;

namespace Rooms.Core.ExcelExporters.ExcelValueTypes;

public interface IExcelValueType
{
    void WriteToExcel(IRow row, int colNumber);
}
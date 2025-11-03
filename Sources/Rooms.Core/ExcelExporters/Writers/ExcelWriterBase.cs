using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using Rooms.Core.ExcelExporters.ExcelValueTypes;

namespace Rooms.Core.ExcelExporters.Writers;

public abstract class ExcelWriterBase<TExportDto>
{
    public virtual List<string> ColumnNames { get; } = [];

    public virtual void Write(ISheet worksheet, TExportDto[] dtos)
    {
        var style = worksheet.Workbook.CreateCellStyle();
        style.FillBackgroundColor = HSSFColor.Yellow.Index;
        style.FillPattern = FillPattern.SolidForeground;
        var headerRow = worksheet.CreateRow(0);
        var columns = ColumnNames.Select((x, i) => new { Name = x, Index = i });
        foreach (var column in columns)
        {
            var cell = headerRow.CreateCell(column.Index);
            cell.SetCellValue(column.Name);
        }

        for (var rowNumber = 1; rowNumber <= dtos.Length; rowNumber++)
        {
            var row = worksheet.CreateRow(rowNumber);
            var mappedValues = MapCellValues(dtos[rowNumber - 1]);

            foreach (var mappedValue in mappedValues)
            {
                mappedValue.Value.WriteToExcel(row, mappedValue.ColumnNumber);
            }
        }
    }

    protected abstract IEnumerable<ColumnCellData> MapCellValues(TExportDto dto);

    protected record ColumnCellData(int ColumnNumber, IExcelValueType Value);
}
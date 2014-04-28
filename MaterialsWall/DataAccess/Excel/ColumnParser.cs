using System.Collections.Generic;
using OfficeOpenXml;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public interface IColumnParser
    {
        IEnumerable<Column> ParseColumns(ExcelWorksheet worksheet);
    }

    public sealed class ColumnParser : IColumnParser
    {
        public IEnumerable<Column> ParseColumns(ExcelWorksheet worksheet)
        {
            var columns = new List<Column>();

            for (int columnIndex = 1; columnIndex <= worksheet.Dimension.End.Column; columnIndex++)
            {
                string columnName = worksheet.Cells[1, columnIndex].GetValue<string>() ?? string.Empty;
                var column = new Column(columnName.ToUpper(), columnIndex);
                columns.Add(column);
            }

            return columns;
        }
    }
}

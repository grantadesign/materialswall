using System;
using System.Collections.Generic;
using System.Linq;
using Granta.MaterialsWall.Models;
using OfficeOpenXml;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public sealed class RowParser
    {
        private readonly IDictionary<string, Column> columns;

        public RowParser(IDictionary<string, Column> columns)
        {
            this.columns = columns;
        }

        public Card ParseRow(ExcelWorksheet worksheet, int rowIndex)
        {
            var columnNames = new ColumnNames();
            var visible = GetColumnValue(worksheet, rowIndex, columnNames.Visible);

            if (CardIsHidden(visible))
            {
                return null;
            }

            var identifier = GetColumnValue(worksheet, rowIndex, columnNames.Identifier);
            var name = GetColumnValue(worksheet, rowIndex, columnNames.Name);
            var id = GetColumnValue(worksheet, rowIndex, columnNames.Id);
            var description = GetColumnValue(worksheet, rowIndex, columnNames.Description);
            var typicalUses = GetColumnValue(worksheet, rowIndex, columnNames.TypicalUses);
            var sample = GetColumnValue(worksheet, rowIndex, columnNames.Sample);
            var source = GetColumnValue(worksheet, rowIndex, columnNames.Source);
            var path = GetColumnValue(worksheet, rowIndex, columnNames.Path);
            var links = GetLinks(columnNames, worksheet, rowIndex);

            var cardFactory = new CardFactory();
            return cardFactory.Create(identifier, name, id, description, typicalUses, source, sample, path, links);
        }

        private static bool CardIsHidden(string visible)
        {
            return !string.Equals(visible, "yes", StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetColumnValue(ExcelWorksheet worksheet, int rowIndex, string columnName)
        {
            Column column;
            return columns.TryGetValue(columnName, out column) ? ExtractCellValue(worksheet, rowIndex, column.Index) : null;
        }

        private string ExtractCellValue(ExcelWorksheet worksheet, int rowIndex, int columnIndex)
        {
            string value = worksheet.Cells[rowIndex, columnIndex].GetValue<string>();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private Link[] GetLinks(ColumnNames columnNames, ExcelWorksheet worksheet, int rowIndex)
        {
            Link[] links =
            {
                GetLink(worksheet, rowIndex, columnNames.Link1Url, columnNames.Link1Name),
                GetLink(worksheet, rowIndex, columnNames.Link2Url, columnNames.Link2Name),
                GetLink(worksheet, rowIndex, columnNames.Link3Url, columnNames.Link3Name)
            };

            return links.Where(l => l != null).ToArray();
        }

        private Link GetLink(ExcelWorksheet worksheet, int rowIndex, string urlColumnName, string textColumnName)
        {
            string url = GetColumnValue(worksheet, rowIndex, urlColumnName);
            string text = GetColumnValue(worksheet, rowIndex, textColumnName);
            return string.IsNullOrWhiteSpace(url) ? null : new Link(url, text);
        }
    }
}

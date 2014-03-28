using System;
using System.Collections.Generic;
using System.Linq;
using Granta.MaterialsWall.Images;
using Granta.MaterialsWall.Models;
using OfficeOpenXml;

namespace Granta.MaterialsWall.DataAccess.Excel
{
    public interface IRowParser
    {
        Card ParseRow(IEnumerable<Column> columns, ExcelWorksheet worksheet, int rowIndex);
    }

    public sealed class RowParser : IRowParser
    {
        private const int MaximumNumberOfImages = 3;

        private readonly ICardFactory cardFactory;
        private readonly IColumnNames columnNames;

        public RowParser(ICardFactory cardFactory, IColumnNames columnNames)
        {
            if (cardFactory == null)
            {
                throw new ArgumentNullException("cardFactory");
            }

            if (columnNames == null)
            {
                throw new ArgumentNullException("columnNames");
            }
            
            this.cardFactory = cardFactory;
            this.columnNames = columnNames;
        }

        public Card ParseRow(IEnumerable<Column> columns, ExcelWorksheet worksheet, int rowIndex)
        {
            var columnsMap = columns.ToDictionary(c => c.Name, c => c);
            
            var visibleColumn = GetColumn(columnsMap, columnNames.Visible);
            var visible = GetColumnValue(visibleColumn, worksheet, rowIndex);

            if (CardIsHidden(visible))
            {
                return null;
            }

            var identifierColumn = GetColumn(columnsMap, columnNames.Identifier);
            var identifier = GetColumnValue(identifierColumn, worksheet, rowIndex);
            
            var nameColumn = GetColumn(columnsMap, columnNames.Name);
            var name = GetColumnValue(nameColumn, worksheet, rowIndex);
            
            var idColumn = GetColumn(columnsMap, columnNames.Id);
            var id = GetColumnValue(idColumn, worksheet, rowIndex);
            
            var descriptionColumn = GetColumn(columnsMap, columnNames.Description);
            var description = GetColumnValue(descriptionColumn, worksheet, rowIndex);
            
            var typicalUsesColumn = GetColumn(columnsMap, columnNames.TypicalUses);
            var typicalUses = GetColumnValue(typicalUsesColumn, worksheet, rowIndex);
            
            var sampleColumn = GetColumn(columnsMap, columnNames.Sample);
            var sample = GetColumnValue(sampleColumn, worksheet, rowIndex);
            
            var sourceColumn = GetColumn(columnsMap, columnNames.Source);
            var source = GetColumnValue(sourceColumn, worksheet, rowIndex);
            
            var pathColumn = GetColumn(columnsMap, columnNames.Path);
            var path = GetColumnValue(pathColumn, worksheet, rowIndex);
            
            var images = GetImages(id);

            var links = GetLinks(columnsMap, worksheet, rowIndex);

            return cardFactory.Create(identifier, name, id, description, typicalUses, source, sample, path, images, links);
        }

        private static bool CardIsHidden(string visible)
        {
            return !string.Equals(visible, "yes", StringComparison.InvariantCultureIgnoreCase);
        }

        private Column GetColumn(IDictionary<string, Column> columnsMap, string columnName)
        {
            Column column;
            columnsMap.TryGetValue(columnName, out column);
            return column;
        }

        private string GetColumnValue(Column column, ExcelWorksheet worksheet, int rowIndex)
        {
            return column != null ? ExtractCellValue(worksheet, rowIndex, column.Index) : null;
        }

        private string ExtractCellValue(ExcelWorksheet worksheet, int rowIndex, int columnIndex)
        {
            string value = worksheet.Cells[rowIndex, columnIndex].GetValue<string>();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private Image[] GetImages(string materialId)
        {
            var images = new List<Image>();
            var imagePathFormatter = new ImagePathFormatter();
            
            for (int index = 1; index <= MaximumNumberOfImages; index++)
            {
                if (imagePathFormatter.DoesImageExist(materialId, index))
                {
                    images.Add(new Image(index));
                }
            }

            return images.ToArray();
        }

        private Link[] GetLinks(IDictionary<string, Column> columnsMap, ExcelWorksheet worksheet, int rowIndex)
        {
            Link[] links =
            {
                GetLink(worksheet, rowIndex, GetColumn(columnsMap, columnNames.Link1Url), GetColumn(columnsMap, columnNames.Link1Name)),
                GetLink(worksheet, rowIndex, GetColumn(columnsMap, columnNames.Link2Url), GetColumn(columnsMap, columnNames.Link2Name)),
                GetLink(worksheet, rowIndex, GetColumn(columnsMap, columnNames.Link3Url), GetColumn(columnsMap, columnNames.Link3Name))
            };

            return links.Where(l => l != null).ToArray();
        }

        private Link GetLink(ExcelWorksheet worksheet, int rowIndex, Column urlColumn, Column textColumn)
        {
            string url = GetColumnValue(urlColumn, worksheet, rowIndex);
            string text = GetColumnValue(textColumn, worksheet, rowIndex);
            return string.IsNullOrWhiteSpace(url) ? null : new Link(url, text);
        }
    }
}

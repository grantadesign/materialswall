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
        private readonly ICardFactory cardFactory;
        private readonly IColumnNames columnNames;
        private readonly IImagePresenceChecker imagePresenceChecker;
        private readonly IMaximumNumberOfImagesPerMaterialProvider maximumNumberOfImagesProvider;

        public RowParser(ICardFactory cardFactory, IColumnNames columnNames, IImagePresenceChecker imagePresenceChecker, IMaximumNumberOfImagesPerMaterialProvider maximumNumberOfImagesProvider)
        {
            if (cardFactory == null)
            {
                throw new ArgumentNullException("cardFactory");
            }

            if (columnNames == null)
            {
                throw new ArgumentNullException("columnNames");
            }

            if (imagePresenceChecker == null)
            {
                throw new ArgumentNullException("imagePresenceChecker");
            }

            if (maximumNumberOfImagesProvider == null)
            {
                throw new ArgumentNullException("maximumNumberOfImagesProvider");
            }
            
            this.cardFactory = cardFactory;
            this.columnNames = columnNames;
            this.imagePresenceChecker = imagePresenceChecker;
            this.maximumNumberOfImagesProvider = maximumNumberOfImagesProvider;
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
            
            var notesColumn = GetColumn(columnsMap, columnNames.Notes);
            var notes = GetColumnValue(notesColumn, worksheet, rowIndex);
            
            var pathColumn = GetColumn(columnsMap, columnNames.Path);
            var path = GetColumnValue(pathColumn, worksheet, rowIndex);
            
            var images = GetImages(id);

            var links = GetLinks(columnsMap, worksheet, rowIndex);

            return cardFactory.Create(identifier, name, id, description, typicalUses, source, sample, notes, path, images, links);
        }

        private static bool CardIsHidden(string visible)
        {
            return !string.Equals(visible, "yes", StringComparison.InvariantCultureIgnoreCase);
        }

        private Column GetColumn(IDictionary<string, Column> columnsMap, string columnName)
        {
            Column column;
            columnsMap.TryGetValue(columnName.ToUpper(), out column);
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
            // there is always at least one image, even if it is the "missing image"
            var images = new List<Image> { new Image(1) };
            int maximumNumberOfImages = maximumNumberOfImagesProvider.GetMaximumNumberOfImagesPerMaterial();

            for (int index = 2; index <= maximumNumberOfImages; index++)
            {
                if (imagePresenceChecker.DoesImageExist(materialId, index))
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

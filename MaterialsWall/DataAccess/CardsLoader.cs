using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Granta.MaterialsWall.DataAccess.Excel;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall.DataAccess
{
    public interface ICardsLoader
    {
        IDictionary<Guid, Card> LoadCards();
    }

    public sealed class CardsLoader : ICardsLoader
    {
        private readonly IAppSettingsProvider appSettingsProvider;
        private readonly IExcelImporter excelImporter;

        public CardsLoader(IAppSettingsProvider appSettingsProvider, IExcelImporter excelImporter)
        {
            if (appSettingsProvider == null)
            {
                throw new ArgumentNullException("appSettingsProvider");
            }

            if (excelImporter == null)
            {
                throw new ArgumentNullException("excelImporter");
            }
            
            this.appSettingsProvider = appSettingsProvider;
            this.excelImporter = excelImporter;
        }

        public IDictionary<Guid, Card> LoadCards()
        {
            string dataFilePath = appSettingsProvider.GetSetting("DataFile");
            string dataFileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFilePath);

            var allCards = excelImporter.ParseData(dataFileFullPath);
            return allCards.ToDictionary(c => c.Identifier, c => c);
        }
    }
}
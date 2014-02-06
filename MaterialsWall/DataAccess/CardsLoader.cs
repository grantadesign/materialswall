using System;
using System.Collections.Generic;
using System.Linq;
using Granta.MaterialsWall.DataAccess.Excel;
using Granta.MaterialsWall.Models;
using Ninject.Extensions.Logging;

namespace Granta.MaterialsWall.DataAccess
{
    public interface ICardsLoader
    {
        IDictionary<Guid, Card> LoadCards();
    }

    public sealed class CardsLoader : ICardsLoader
    {
        private readonly ILogger logger;
        private readonly IDataFilePathProvider dataFilePathProvider;
        private readonly IExcelImporter excelImporter;

        public CardsLoader(ILogger logger, IDataFilePathProvider dataFilePathProvider, IExcelImporter excelImporter)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
            
            if (dataFilePathProvider == null)
            {
                throw new ArgumentNullException("dataFilePathProvider");
            }

            if (excelImporter == null)
            {
                throw new ArgumentNullException("excelImporter");
            }

            this.logger = logger;
            this.dataFilePathProvider = dataFilePathProvider;
            this.excelImporter = excelImporter;
        }

        public IDictionary<Guid, Card> LoadCards()
        {
            string dataFilePath = dataFilePathProvider.GetPath();
            logger.Info("Loading cards from '{0}'", dataFilePath);
            var cards = excelImporter.ParseData(dataFilePath);
            return cards.ToDictionary(c => c.Identifier, c => c);
        }
    }
}

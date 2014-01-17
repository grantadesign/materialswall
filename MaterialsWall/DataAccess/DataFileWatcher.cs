using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Granta.MaterialsWall.DataAccess
{
    public sealed class DataFileWatcher
    {
        private static readonly DataFileWatcher instance = new DataFileWatcher();

        public static IEnumerable<Card> Cards{get {return instance.GetCards();}}

        private const string DataFileName = "data.txt";

        private readonly FileSystemWatcher fileSystemWatcher;
        private readonly string dataFileDirectory;

        private bool isDirty;
        private IEnumerable<Card> cards;

        private DataFileWatcher()
        {
            dataFileDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/");
            fileSystemWatcher = new FileSystemWatcher(dataFileDirectory)
                      {
                            NotifyFilter = NotifyFilters.Attributes | NotifyFilters.DirectoryName 
                                            | NotifyFilters.FileName | NotifyFilters.LastWrite 
                                            | NotifyFilters.Security | NotifyFilters.Size,
                            Filter = DataFileName
                      };
            fileSystemWatcher.Changed += (sender, e) => isDirty = true;
            fileSystemWatcher.EnableRaisingEvents = true;
            ReloadCards();
        }

        public IEnumerable<Card> GetCards()
        {
            if (isDirty)
            {
                ReloadCards();
            }

            return cards;
        }

        private void ReloadCards()
        {
            var newCards = ReloadCardsFromFile();

            lock (syncroot)
            {
                cards = newCards;
            }
        }

        private IEnumerable<Card> ReloadCardsFromFile()
        {
            string dataFilePath = Path.Combine(dataFileDirectory, DataFileName);
            string[] lines = File.ReadAllLines(dataFilePath);
            return lines.Skip(1).Select(ParseCard).ToList();
        }

        private Card ParseCard(string line)
        {
            string[] parts = line.Split('\t');
            return new Card
            {
                Identifier = Guid.NewGuid(),
                Name = GetValueOrNullIfNotSet(parts[0]),
                Path = GetValueOrNullIfNotSet(parts[7]),
                Description = GetValueOrNullIfNotSet(parts[3]),
                TypicalUses = GetValueOrNullIfNotSet(parts[4]),
                Source = GetValueOrNullIfNotSet(parts[6]),
//                Sample = GetValueOrNullIfNotSet(parts[8]),
                Links = new[] {"http://a.sapmple.link"}
            };
        }

        private string GetValueOrNullIfNotSet(string value)
        {
            value = value ?? string.Empty;
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim('"');
        }

        private readonly object syncroot = new object();
    }
}

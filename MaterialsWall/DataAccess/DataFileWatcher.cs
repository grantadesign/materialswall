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
            return lines.Skip(1).Select(ParseCard).Where(c => c != null).ToList();
        }

        private const int includeInWallColumn = 1;
        private const int guidColumn = 3;
        private const int nameColumn = 0;
        private const int idColumn = 2;
        private const int descriptionColumn = 4;
        private const int typicalUsesColumn = 5;
        private const int sampleColumn = 6;
        private const int sourceColumn = 7;
        private const int materialUniversePathColumn = 8;
        private const int link1Column = 9;
        private const int link2Column = 10;
        private const int link3Column = 11;

        private Card ParseCard(string line)
        {
            string[] parts = line.Split('\t');
            var includeInWall = GetValueOrNullIfNotSet(parts[includeInWallColumn]);

            if (string.IsNullOrEmpty(includeInWall) || !string.Equals(includeInWall, "yes", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            var identifier = GetValueOrNullIfNotSet(parts[guidColumn]);

            if (identifier == null)
            {
                return null;
            }

            return new Card
            {
                Identifier = Guid.Parse(identifier),
                Id = GetValueOrNullIfNotSet(parts[idColumn]),
                Name = GetValueOrNullIfNotSet(parts[nameColumn]),
                Description = GetValueOrNullIfNotSet(parts[descriptionColumn]),
                TypicalUses = GetValueOrNullIfNotSet(parts[typicalUsesColumn]),
                Sample = GetValueOrNullIfNotSet(parts[sampleColumn]),
                Source = GetValueOrNullIfNotSet(parts[sourceColumn]),
                Path = GetValueOrNullIfNotSet(parts[materialUniversePathColumn]),
                Links = GetLinks(parts)
            };
        }

        private string[] GetLinks(string[] parts)
        {
            string[] links =
            {
                GetValueOrNullIfNotSet(parts[link1Column]),
                GetValueOrNullIfNotSet(parts[link2Column]),
                GetValueOrNullIfNotSet(parts[link3Column])
            };

            return links.Where(l => l != null).ToArray();
        }

        private string GetValueOrNullIfNotSet(string value)
        {
            value = value ?? string.Empty;
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim('"');
        }

        private readonly object syncroot = new object();
    }
}

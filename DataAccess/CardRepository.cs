using System;
using System.Collections.Generic;

namespace Granta.MaterialsWall.DataAccess
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetCards();
        Card GetCard(Guid identifier);
    }

    public class CardRepository : ICardRepository
    {
        private readonly Dictionary<Guid, Card> cards;

        public CardRepository()
        {
            cards = new Dictionary<Guid, Card>();
            AddCard("E4365B28-2005-4554-858A-CC44548D103B", "Material 1", "", "Description 1", "Typical Uses", "Source");
            AddCard("451695A0-59F8-413A-AF08-BBBF2097394D", "Material 2", "MaterialUniverse > Polymers > Plastics > Thermoplastics", "", "Typical Uses", "Source");
            AddCard("A9DB2D2E-8464-45C6-8D5F-1D4477B639D3", "Material 3", "", "Description 1", "Typical Uses", "Source");
            AddCard("4C32EA58-A21B-48A2-94A4-2BF0491B5A6E", "Material 4", "MaterialUniverse > Some > Path > To > Material", "Description 1", "", "Source");
            AddCard("BD976E4C-9A74-4FE4-A71D-A12298A05F88", "Material 5", "", "", "", "");
            AddCard("582599B6-2984-4FA2-B836-37868A4EF190", "Material 6", "MaterialUniverse > Path", "Description 6", "Typical Uses", "Source");
        }

        private void AddCard(string id, string name, string path, string description, string typicalUses, string source)
        {
            var identifier = Guid.Parse(id);
            var card = new Card
            {
                Identifier = identifier,
                Name = name,
                Path = string.IsNullOrWhiteSpace(path) ? null : path,
                Description = string.IsNullOrWhiteSpace(description) ? null : description,
                TypicalUses = string.IsNullOrWhiteSpace(typicalUses) ? null : typicalUses,
                Source = string.IsNullOrWhiteSpace(source) ? null : source
            };

            cards.Add(identifier, card);
        }

        public IEnumerable<Card> GetCards()
        {
            return cards.Values;
        }

        public Card GetCard(Guid identifier)
        {
            Card card;
            cards.TryGetValue(identifier, out card);
            return card;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

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
            IEnumerable<Card> allCards = DataFileWatcher.Cards;
            cards = allCards.ToDictionary(c => c.Identifier, c => c);
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

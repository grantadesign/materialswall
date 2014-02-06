using System;
using System.Collections.Generic;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall.DataAccess
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetCards();
        Card GetCard(Guid identifier);
    }

    public sealed class CardRepository : ICardRepository
    {
        private readonly ICardsLoader cardsLoader;
        private readonly IDataFileWatcher dataFileWatcher;

        private IDictionary<Guid, Card> cards;

        public CardRepository(ICardsLoader cardsLoader, IDataFileWatcher dataFileWatcher)
        {
            if (cardsLoader == null)
            {
                throw new ArgumentNullException("cardsLoader");
            }
            
            if (dataFileWatcher == null)
            {
                throw new ArgumentNullException("dataFileWatcher");
            }

            this.cardsLoader = cardsLoader;
            this.dataFileWatcher = dataFileWatcher;
        }

        public IEnumerable<Card> GetCards()
        {
            ReloadCardsIfNecessary();
            return cards.Values;
        }

        public Card GetCard(Guid identifier)
        {
            ReloadCardsIfNecessary();
            Card card;
            cards.TryGetValue(identifier, out card);
            return card;
        }

        private void ReloadCardsIfNecessary()
        {
            if (dataFileWatcher.FileHasChanged)
            {
                var newCards = cardsLoader.LoadCards();

                lock (syncroot)
                {
                    cards = newCards;
                    dataFileWatcher.FileReloaded();
                }
            }
        }

        private readonly object syncroot = new object();
    }
}

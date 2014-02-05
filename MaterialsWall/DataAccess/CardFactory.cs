using System;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall.DataAccess
{
    public interface ICardFactory
    {
        Card Create(string identifier, string name, string id, string description, string typicalUses, string source, string sample, string path, Link[] links);
    }

    public sealed class CardFactory : ICardFactory
    {
        public Card Create(string identifier, string name, string id, string description, string typicalUses, string source, string sample, string path, Link[] links)
        {
            Guid guid = Guid.Empty;
            bool identifierIsInvalid = string.IsNullOrWhiteSpace(identifier) || !Guid.TryParse(identifier, out guid);
            bool hasNoName = string.IsNullOrWhiteSpace(name);

            if (identifierIsInvalid || hasNoName)
            {
                return null;
            }

            return new Card(guid, name, id, description, typicalUses, source, sample, path, links);
        }
    }
}

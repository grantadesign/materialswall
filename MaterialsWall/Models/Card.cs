using System;

namespace Granta.MaterialsWall.Models
{
    public sealed class Card
    {
        public Guid Identifier{get { return identifier; }}
        private readonly Guid identifier;

        public string Name{get { return name; }}
        private readonly string name;

        public string Id{get { return id; }}
        private readonly string id;

        public string Description{get { return description; }}
        private readonly string description;

        public string TypicalUses{get { return typicalUses; }}
        private readonly string typicalUses;

        public string Source{get { return source; }}
        private readonly string source;

        public string Sample{get { return sample; }}
        private readonly string sample;

        public string Path{get { return path; }}
        private readonly string path;

        public Link[] Links{get { return links; }}
        private readonly Link[] links;

        public Card(Guid identifier, string name, string id, string description, string typicalUses, string source, string sample, string path, Link[] links)
        {
            this.identifier = identifier;
            this.name = name;
            this.id = id;
            this.description = description;
            this.typicalUses = typicalUses;
            this.source = source;
            this.sample = sample;
            this.path = path;
            this.links = links;
        }
    }
}

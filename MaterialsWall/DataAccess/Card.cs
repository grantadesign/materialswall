using System;

namespace Granta.MaterialsWall.DataAccess
{
    public sealed class Card
    {
        public Guid Identifier{get;set;}
        public string Name{get;set;}
        public string Path{get;set;}
        public string Description{get;set;}
        public string TypicalUses{get;set;}
        public string Source{get;set;}
        public string[] Links{get;set;}
    }
}

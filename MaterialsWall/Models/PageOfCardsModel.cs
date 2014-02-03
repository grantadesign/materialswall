using System.Collections.Generic;
using Granta.MaterialsWall.DataAccess;

namespace Granta.MaterialsWall.Models
{
    public sealed class PageOfCardsModel
    {
        public int PageNumber{get;set;}
        public IEnumerable<Card> Cards{get;set;}
    }
}

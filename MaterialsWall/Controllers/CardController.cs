using System;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class CardController : Controller
    {
        private readonly ICardRepository cardRepository;

        public CardController(ICardRepository cardRepository)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }
            
            this.cardRepository = cardRepository;
        }

        public ActionResult Index(Guid identifier)
        {
            var card = cardRepository.GetCard(identifier);
            return View(card);
        }
    }
}
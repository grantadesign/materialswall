using System;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ICardRepository cardRepository;

        public HomeController(ICardRepository cardRepository)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }
            
            this.cardRepository = cardRepository;
        }

        public ActionResult Index()
        {
            var cards = cardRepository.GetCards();
            return View(cards);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
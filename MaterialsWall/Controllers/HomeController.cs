using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;
using Granta.MaterialsWall.Models;

namespace Granta.MaterialsWall.Controllers
{
    public sealed class HomeController : Controller
    {
        private const int PageSize = 12;

        private readonly ICardRepository cardRepository;
        private readonly IPaginator<Card> paginator;

        public HomeController(ICardRepository cardRepository, IPaginator<Card> paginator)
        {
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }

            if (paginator == null)
            {
                throw new ArgumentNullException("paginator");
            }
            
            this.cardRepository = cardRepository;
            this.paginator = paginator;
        }

        public ActionResult Index()
        {
            var cards = GetCardsOnPage(1);
            return View(cards);
        }

        public ActionResult Page(int p = 1)
        {
            var cards = GetCardsOnPage(p);
            var model = new PageOfCardsModel {PageNumber = p, Cards = cards};
            return View(model);
        }

        private IEnumerable<Card> GetCardsOnPage(int pageNumber)
        {
            var cards = cardRepository.GetCards().ToList();
            var pagedCards = paginator.GetPage(PageSize, pageNumber, cards);
            return pagedCards;
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
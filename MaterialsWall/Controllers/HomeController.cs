using System;
using System.Linq;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;

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

        public ActionResult Index(int p = 1)
        {
            var cards = cardRepository.GetCards().ToList();
            var pagedCards = paginator.GetPage(PageSize, p, cards);
            return View(pagedCards);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
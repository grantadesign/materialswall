using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;

namespace Granta.MaterialsWall.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cards = new CardRepository().GetCards();
            return View(cards);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
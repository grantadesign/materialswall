using System;
using System.Web.Mvc;
using Granta.MaterialsWall.DataAccess;

namespace Granta.MaterialsWall.Controllers
{
    public class CardController : Controller
    {
        public ActionResult Index(Guid identifier)
        {
            var card = new CardRepository().GetCard(identifier);
            return View(card);
        }
    }
}
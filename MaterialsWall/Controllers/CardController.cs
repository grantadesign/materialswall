using System;
using System.Web.Mvc;

namespace Granta.MaterialsWall.Controllers
{
    public class CardController : Controller
    {
        public ActionResult Index(Guid identifier)
        {
            return View();
        }
    }
}
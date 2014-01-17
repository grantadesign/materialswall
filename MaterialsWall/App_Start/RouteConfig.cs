using System.Web.Mvc;
using System.Web.Routing;

namespace Granta.MaterialsWall
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{identifier}",
                defaults: new { controller = "Home", action = "Index", identifier = UrlParameter.Optional }
            );
        }
    }
}
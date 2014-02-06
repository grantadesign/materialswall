using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace Granta.MaterialsWall
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var logger = LogManager.GetLogger("default");
            logger.Debug("Application starting...");

            AreaRegistration.RegisterAllAreas();

            NinjectConfig.Start(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
         
            logger.Info("Application started");
        }
    }
}

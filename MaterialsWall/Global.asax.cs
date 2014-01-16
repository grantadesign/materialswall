using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Granta.MaterialsWall.Ninject;
using Ninject.Web.Mvc;

namespace Granta.MaterialsWall
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var kernel = NinjectConfig.Start();

            HttpConfiguration configuration = GlobalConfiguration.Configuration;
            configuration.DependencyResolver = new DependencyResolverAdapter(new NinjectDependencyResolver(kernel));
            
            WebApiConfig.Register(configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

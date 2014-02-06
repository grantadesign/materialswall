using System.Web.Mvc;
using Granta.MaterialsWall.Logging;

namespace Granta.MaterialsWall
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            FilterProviders.Providers.Add(new LoggingFilterProvider());
            filters.Add(new HandleErrorAttribute());
        }
    }
}

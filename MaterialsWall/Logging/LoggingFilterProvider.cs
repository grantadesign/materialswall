using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Granta.MaterialsWall.Controllers;

namespace Granta.MaterialsWall.Logging
{
    public sealed class LoggingFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var route = controllerContext.RequestContext.RouteData.Route as Route;
            return controllerContext.Controller is CardController
                && route != null && route.Url != null && route.Url.StartsWith("QR/", StringComparison.InvariantCultureIgnoreCase)
                        ? new[] {new Filter(new LogRequestFilter(), FilterScope.Global, null)} 
                        : new Filter[0];
        }
    }
}

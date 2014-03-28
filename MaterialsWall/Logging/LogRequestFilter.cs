using System;
using System.Text;
using System.Web.Mvc;
using NLog;

namespace Granta.MaterialsWall.Logging
{
    public sealed class LogRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var logger = LogManager.GetLogger("requests");
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var sb = new StringBuilder();
            sb.AppendLine("Request via QR code:");
            sb.AppendFormat("    Request:  {0}{1}", request.Url.AbsoluteUri, Environment.NewLine);
            sb.AppendFormat("              {0}{1}", request.UserHostAddress, Environment.NewLine);
            sb.AppendFormat("              {0}{1}", request.UserAgent, Environment.NewLine);
            sb.AppendFormat("    Response: {0}{1}", response.StatusCode, Environment.NewLine);
            logger.Info(sb.ToString());
        }
    }
}

using System.Web;
using Ninject.Modules;

namespace Granta.MaterialsWall.Ninject
{
    public sealed class HttpModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<HttpServerUtility>().ToMethod(ctx => HttpContext.Current.Server);
        }
    }
}

using System;
using Granta.MaterialsWall.Logging;
using Ninject.Activation;
using Ninject.Extensions.Logging;
using Ninject.Modules;
using NLog;

namespace Granta.MaterialsWall.Ninject
{
    public sealed class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(new Func<IContext, ILogger>(a => new NLogToNinjectLogConverter(LogManager.GetLogger("default"))));
        } 
    }
}

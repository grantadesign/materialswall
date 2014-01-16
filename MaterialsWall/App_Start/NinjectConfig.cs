using System;
using Ninject;
using Ninject.Web.Common;

namespace Granta.MaterialsWall
{
    internal static class NinjectConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Start() 
        {
            bootstrapper.Initialize(CreateKernel);
            return bootstrapper.Kernel;
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true });
            kernel.Load(typeof(NinjectConfig).Assembly);
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            return kernel;
        }
    }
}

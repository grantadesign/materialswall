using System;
using System.Web.Http;
using Granta.MaterialsWall.Ninject;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;

namespace Granta.MaterialsWall
{
    internal static class NinjectConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Start(HttpConfiguration configuration) 
        {
            bootstrapper.Initialize(CreateKernel);
            IKernel kernel = bootstrapper.Kernel;
            configuration.DependencyResolver = new DependencyResolverAdapter(new NinjectDependencyResolver(kernel));
            return kernel;
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

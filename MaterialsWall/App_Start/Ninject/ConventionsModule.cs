using Granta.MaterialsWall.DataAccess;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Granta.MaterialsWall.Ninject
{
    public sealed class ConventionsModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(scanner => scanner.FromAssembliesMatching("Granta.MaterialsWall*.dll")
                                               .SelectAllClasses()
                                               .Excluding<CardRepository>()
                                               .Excluding<DataFileWatcher>()
                                               .BindDefaultInterface()
                                               .Configure(binding => binding.InRequestScope()));
        }
    }
}

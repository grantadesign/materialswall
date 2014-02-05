using Granta.MaterialsWall.DataAccess;
using Ninject.Modules;

namespace Granta.MaterialsWall.Ninject
{
    public sealed class DataLoadingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ICardRepository>().To<CardRepository>().InSingletonScope();
            Kernel.Bind<IDataFileWatcher>().To<DataFileWatcher>().InSingletonScope();
        }
    }
}
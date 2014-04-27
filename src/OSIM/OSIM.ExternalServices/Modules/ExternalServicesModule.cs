using Ninject.Modules;

namespace OSIM.ExternalServices.Modules
{
    public class ExternalServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInventoryService>().To<InventoryService>();
        }
    }
}
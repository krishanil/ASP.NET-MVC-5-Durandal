using Ninject.Modules;
using WebApplication.DAL.Repositories;

namespace WebApplication.BLL.IoC
{
    class BussinesLayerLayerDependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdminRepository>().To<AdminRepository>();
        }
    }
}

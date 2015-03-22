using Ninject.Modules;
using WebApplication.DAL.Repositories;
using WebApplication.DAL.Repositories.AccountRepository;

namespace WebApplication.BLL.IoC
{
    class BussinesLayerDependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IAdminRepository>().To<AdminRepository>();
        }
    }
}

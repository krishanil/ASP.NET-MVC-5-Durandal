using System.Data.Entity;
using Ninject.Modules;
using WebApplication.DAL.DataContext;
using WebApplication.DAL.DataContext.AccountContext;
using WebApplication.DAL.Repositories.AccountRepository;
using WebApplication.DAL.Repositories.AdminRepository;

namespace WebApplication.BLL.IoC
{
    public class BussinesLayerDependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<AppDBEntities>();

            Bind<IAccountRepository>().To<AccountRepository>();
            Bind<IAccountContext>().To<AppIdentityDbContext>();

            Bind<IAdminRepository>().To<AdminRepository>();
        }
    }
}

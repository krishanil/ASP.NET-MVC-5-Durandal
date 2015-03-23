using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.BLL.Managers;
using WebApplication.BLL.Managers.Account;

namespace WebApplication.IoC
{
    public class ViewLayerDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public ViewLayerDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
//            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        private void AddBindings()
        {
            kernel.Bind<IAccountManager>().To<AccountManager>();
            kernel.Bind<IAdminManager>().To<AdminManager>();
        }
    }
}
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject.Web.Common;
using WebApplication.BLL.Managers.Account;
using WebApplication.BLL.Managers.Admin;

namespace WebApplication.IoC
{
    public class VlDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public VlDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
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

            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<AppUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>()).InRequestScope();
        }
    }
}
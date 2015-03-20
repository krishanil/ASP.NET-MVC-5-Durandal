using System.Web.Optimization;
using WebApplication;

[assembly: WebActivator.PostApplicationStartMethod(typeof(DurandalConfig), "PreStart")]

namespace WebApplication
{
    public static class DurandalConfig
    {
        public static void PreStart()
        {
            // Add your start logic here
            DurandalBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
using System.Web.Optimization;

[assembly: WebActivator.PostApplicationStartMethod(typeof(WebApplication.DurandalConfig), "PreStart")]

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
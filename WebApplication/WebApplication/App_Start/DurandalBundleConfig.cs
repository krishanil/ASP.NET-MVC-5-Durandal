using System;
using System.Web.Optimization;

namespace WebApplication
{
    public class DurandalBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/lib/modernizr-*"));

            bundles.Add(
              new ScriptBundle("~/Scripts/vendor")
                  .Include("~/Scripts/lib/jquery-{version}.js")
                  .Include("~/Scripts/lib/bootstrap.js")
                  .Include("~/Scripts/lib/respond.js")
                  .Include("~/Scripts/lib/knockout-{version}.js")
                  .Include("~/Scripts/lib/jquery.livequery.js")
                  .Include("~/Scripts/lib/floatLabel.js")
              );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/starterkit.css")
                .Include("~/Content/Site.css")
                .Include("~/Content/floatLabel.css")
              );
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}
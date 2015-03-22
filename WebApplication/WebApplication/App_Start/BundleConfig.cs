using System;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/lib/jquery-{version}.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/lib/bootstrap.js")
                .Include("~/Scripts/lib/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/linq")
                .Include("~/Scripts/lib/linq/linq.js")
                .Include("~/Scripts/lib/linq/jquery.linq.js"));

            bundles.Add(new ScriptBundle("~/bundles/global")
                .Include("~/Scripts/global/controls-behavior.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/starterkit.css")
                .Include("~/Content/toastr.min.css")
                .Include("~/Content/Site.css")
              );

            bundles.Add(new StyleBundle("~/Content/css/controls")
                .Include("~/Content/controls/buttons.css")
                .Include("~/Content/controls/checkbox.css")
                .Include("~/Content/controls/label.css")
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

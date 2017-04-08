using System.Web.Optimization;

namespace FT.MvcApp {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/reset.css",
                "~/Content/pager.css",
                "~/Content/site.css"
            ));

            bundles.Add(new ScriptBundle("~/script/jquery-lib").Include(
                "~/Scripts/jquery-3.1.1.min.js"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
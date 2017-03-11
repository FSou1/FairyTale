using System.Web.Optimization;

namespace FT.MvcApp {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/reset.css",
                "~/Content/pager.css",
                "~/Content/site.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
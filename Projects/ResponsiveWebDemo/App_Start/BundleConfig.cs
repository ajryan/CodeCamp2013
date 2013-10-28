using System.Web.Optimization;

namespace ResponsiveWebDesignDemo.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // SCRIPT BUNDLES
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.hotkeys.js"));

            bundles.Add(new ScriptBundle("~/bundles/handlebars").Include(
                        "~/Scripts/handlebars.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/holder.js"));

            // STYLE BUNDLES
            bundles.Add(new StyleBundle("~/bundles/base").Include(
                "~/Content/reset.css",
                "~/Content/site.css"));
        }
    }
}
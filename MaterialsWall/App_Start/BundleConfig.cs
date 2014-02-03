using System.Web.Optimization;

namespace Granta.MaterialsWall
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/labels").Include(
                        "~/Content/walllabel.css"));

            bundles.Add(new StyleBundle("~/bundles/wallcards").Include(
                        "~/Content/wallcard.css"));

            bundles.Add(new StyleBundle("~/bundles/cards").Include(
                        "~/Content/card.css"));

            bundles.Add(new StyleBundle("~/bundles/about").Include(
                        "~/Content/about.css"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/Content/site.css",
                        "~/Content/materialswall.css"));

            bundles.Add(new ScriptBundle("~/bundles/searchfilter").Include(
                        "~/Scripts/searchfilter.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/scroll-loading").Include(
                        "~/Scripts/scroll-loading.js",
                        "~/Scripts/jquery.infinite-scroll-helper.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
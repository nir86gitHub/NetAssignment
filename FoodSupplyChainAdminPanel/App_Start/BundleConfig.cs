using System.Web;
using System.Web.Optimization;

namespace FoodSupplyChainAdminPanel
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Plugins/MultiSelect/jquery-1.8.3.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/Plugins").Include(
                      "~/Content/Plugins/MultiSelect/bootstrap.min.js",
                      "~/Content/Plugins/MultiSelect/multiselect.js",
                      "~/Content/Plugins/DataTable/datatables.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Plugins/Css").Include(
                      "~/Content/Plugins/MultiSelect/bootstrap.multiselect.min.css",
                      "~/Content/Plugins/DataTable/datatables.min.css"
                      ));


        }
    }
}

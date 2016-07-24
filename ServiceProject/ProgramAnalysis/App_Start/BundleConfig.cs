using System.Web;
using System.Web.Optimization;

namespace ProgramAnalysis
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                       "~/Content/themes/base/jquery.ui.core.css",
                       "~/Content/themes/base/jquery.ui.resizable.css",
                       "~/Content/themes/base/jquery.ui.selectable.css",
                       "~/Content/themes/base/jquery.ui.accordion.css",
                       "~/Content/themes/base/jquery.ui.autocomplete.css",
                       "~/Content/themes/base/jquery.ui.button.css",
                       "~/Content/themes/base/jquery.ui.dialog.css",
                       "~/Content/themes/base/jquery.ui.slider.css",
                       "~/Content/themes/base/jquery.ui.tabs.css",
                       "~/Content/themes/base/jquery.ui.datepicker.css",
                       "~/Content/themes/base/jquery.ui.progressbar.css",
                       "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/Content/KAdmin/script").Include(
                        "~/Content/themes/KAdmin/script/jquery-1.10.2.min.js",
                        "~/Content/themes/KAdmin/script/jquery-migrate-1.2.1.min.js",
                        "~/Content/themes/KAdmin/script/jquery-ui.js",
                        "~/Content/themes/KAdmin/script/bootstrap.min.js",
                        "~/Content/themes/KAdmin/script/bootstrap-hover-dropdown.js",
                        "~/Content/themes/KAdmin/script/html5shiv.js",
                        "~/Content/themes/KAdmin/script/respond.min.js",
                        "~/Content/themes/KAdmin/script/jquery.metisMenu.js",
                        "~/Content/themes/KAdmin/script/jquery.slimscroll.js",
                        "~/Content/themes/KAdmin/script/jquery.cookie.js",
                        "~/Content/themes/KAdmin/script/icheck.min.js",
                        "~/Content/themes/KAdmin/script/custom.min.js",
                        "~/Content/themes/KAdmin/script/jquery.news-ticker.js",
                        "~/Content/themes/KAdmin/script/jquery.menu.js",
                        "~/Content/themes/KAdmin/script/pace.min.js",
                        "~/Content/themes/KAdmin/script/holder.js",
                        "~/Content/themes/KAdmin/script/responsive-tabs.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.categories.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.pie.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.tooltip.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.resize.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.fillbetween.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.stack.js",
                        "~/Content/themes/KAdmin/script/jquery.flot.spline.js",
                        "~/Content/themes/KAdmin/script/zabuto_calendar.min.js",
                        "~/Content/themes/KAdmin/script/index.js",
                //LOADING SCRIPTS FOR CHARTS
                        "~/Content/themes/KAdmin/script/highcharts.js",
                        "~/Content/themes/KAdmin/script/data.js",
                        "~/Content/themes/KAdmin/script/drilldown.js",
                        "~/Content/themes/KAdmin/script/exporting.js",
                        "~/Content/themes/KAdmin/script/highcharts-more.js",
                        "~/Content/themes/KAdmin/script/charts-highchart-pie.js",
                        "~/Content/themes/KAdmin/script/charts-highchart-more.js",
                //CORE JAVASCRIPT
                        "~/Content/themes/KAdmin/script/main.js"));

            bundles.Add(new StyleBundle("~/Content/themes/KAdmin/css").Include(
                        "~/Content/themes/KAdmin/styles/jquery-ui-1.10.4.custom.min.css",
                        "~/Content/themes/KAdmin/styles/font-awesome.min.css",
                        "~/Content/themes/KAdmin/styles/bootstrap.min.css",
                        "~/Content/themes/KAdmin/styles/animate.css",
                        "~/Content/themes/KAdmin/styles/all.css",
                        "~/Content/themes/KAdmin/styles/main.css",
                        "~/Content/themes/KAdmin/styles/style-responsive.css",
                        "~/Content/themes/KAdmin/styles/zabuto_calendar.min.css",
                        "~/Content/themes/KAdmin/styles/pace.css",
                        "~/Content/themes/KAdmin/styles/jquery.news-ticker.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}

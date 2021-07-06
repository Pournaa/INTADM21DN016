using System.Web;
using System.Web.Optimization;

namespace BankOfIntern
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/cse").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/bootstrap-theme.css.map",
                                                                    "~/Content/bootstrap-theme.min.css", "~/Content/bootstrap.css.map", "~/Content/bootstrap.min.css", "~/Content/bootstrap.min.css.map", "~/Content/site.css", "~/Content/Login.css"));
            bundles.Add(new StyleBundle("~/Content/csd").Include("~/Content/BankingStyles.css"));
            bundles.Add(new StyleBundle("~/Content/csopen").Include("~/Content/NewAcc.css"));
            bundles.Add(new StyleBundle("~/Content/csuid").Include("~/Content/EnterUserId.css"));
            bundles.Add(new StyleBundle("~/Content/csnp").Include("~/Content/NewPwd.css"));
            bundles.Add(new StyleBundle("~/Content/csrp").Include("~/Content/ResetPwd.css"));
            bundles.Add(new StyleBundle("~/Content/cslogout").Include("~/Content/Logout.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap1.css", "~/Content/bootstrap-theme.css", "~/Content/bootstrap-theme.css.map",
                                                                    "~/Content/bootstrap-theme.min.css", "~/Content/bootstrap.css.map", "~/Content/bootstrap.min.css", "~/Content/bootstrap.min.css.map", "~/Content/site1.css", "~/Content/BankingStyles.css"));
            BundleTable.EnableOptimizations = true;

        }
    }
}

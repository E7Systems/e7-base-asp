using System.Web.Optimization;

namespace web_mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //StyleBundle being registered as javascript.  
            //Until I figure it out, leave as out.
            //bundles.Add(new StyleBundle("~/content/styles")
            //    .Include("~/Content/bootstrap.css")
            //    .Include("~/Content/Site.css")
            //    );

            bundles.Add(new ScriptBundle("~/scripts")
            .Include("~/scripts/jquery-2.1.3.js")
            .Include("~/scripts/jquery.validate.js")
            .Include("~/scripts/jquery.validate.unobtrusive.js")
            .Include("~/scripts/bootstrap.cs")
            .Include("~/scripts/forms.js")
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web_mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //explicitly set routes here
            //TEMPORARY
            //routes.MapRoute(
            //    name: "home",
            //    url: "",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

            routes.MapRoute(
                name: "projects",
                url: "",
                defaults: new { controller = "Projects", action = "Index" }
            );

            routes.MapRoute(
                name: "login",
                url: "login",
                defaults: new { controller = "Projects", action = "Index" }
            );
        }
    }
}

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

            //explicitly set routes here - gives flexibility while doing early controller development
            //TEMPORARY

            routes.MapRoute(
                name: "project",
                url: "project",
                defaults: new { controller = "Project", action = "Index" }
            );

            routes.MapRoute(
                name: "login",
                url: string.Empty,  //empty string means match anything not explicitly mapped elsewhere
                defaults: new { controller = "Login", action = "Index" }
            );
        }
    }
}

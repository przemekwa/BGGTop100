using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BoardGameRankExplorer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PageRangeApi2",
                url: "{controller}/{action}/{last}",
                defaults: new {controller = "Explore", action = "Range"}
                );

            routes.MapRoute(
               name: "PageRangeApi",
               url: "{controller}/{action}/{first}-{last}",
               defaults: new { controller = "Explore", action = "Range" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Explore", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

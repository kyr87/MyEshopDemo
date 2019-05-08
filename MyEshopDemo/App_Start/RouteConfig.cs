using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyEshopDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Cart", "Cart/{action}/{id}",
                new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                new[] { "MyEshopDemo.Controllers" });

            routes.MapRoute(
                name: "Single Product",
                url: "Home/product/{id}",
                defaults: new { controller = "Home", action = "SingleProduct" }
                );
            routes.MapRoute(
                name: "Single Category",
                url:"Home/category/{id}",
                defaults: new {controller = "Home",action ="SingleCategory"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}

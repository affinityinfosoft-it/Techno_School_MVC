using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



    //        routes.MapRoute(
    //    name: "StudentManagement",
    //    url: "StudentManagement/{action}/{*id}",
    //    defaults: new { controller = "StudentManagement", action = "Index", id = UrlParameter.Optional }
    //);



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

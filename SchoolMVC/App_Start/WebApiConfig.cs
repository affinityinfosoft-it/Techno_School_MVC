using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SchoolMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // ✅ Enable CORS globally
            var cors = new EnableCorsAttribute("*", "*", "*"); // You can restrict origins/headers/methods as needed
            config.EnableCors(cors);
            // Web API configuration and services
           
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("studentportalapi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional, area = "StudentPortal" }
            );


            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }
    }
}

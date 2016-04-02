using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiREST.Unity;

namespace WebApiREST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.DependencyResolver = new UnityResolver(DependencyResolver.DIContainer());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

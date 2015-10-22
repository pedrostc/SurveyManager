using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SurveyManager.Services.Scheduler.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{name}/{groupName}",
                defaults: new { name = RouteParameter.Optional, groupName = RouteParameter.Optional }
            );
        }
    }
}

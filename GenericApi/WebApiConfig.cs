﻿using System.Web.Http;

namespace GenericApi
{
    /// <summary>
    /// Конфигурация Web API
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Регистрация конфигурации
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API
            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

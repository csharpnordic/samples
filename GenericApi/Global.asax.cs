using System.Web;
using System.Web.Http;

namespace GenericApi
{
    /// <summary>
    /// Приложение REST API
    /// </summary>
    public class WebApiApplication : HttpApplication
    {     
        /// <summary>
        /// Запуск приложения
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // https://stackoverflow.com/questions/44920319/the-request-entitys-media-type-text-plain-is-not-supported-for-this-resource
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
#if DEBUG
            // Для отладки выводим JSON, удобный для человека
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .Formatting = Newtonsoft.Json.Formatting.Indented;
#else
            // В релизе используем компактную запись для оптимизации трафика
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .Formatting = Newtonsoft.Json.Formatting.None;
#endif
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);         
        }
    }
}

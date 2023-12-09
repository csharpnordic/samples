using System.Web;
using System.Web.Http;

namespace GenericApi
{
    /// <summary>
    /// ���������� REST API
    /// </summary>
    public class WebApiApplication : HttpApplication
    {     
        /// <summary>
        /// ������ ����������
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // https://stackoverflow.com/questions/44920319/the-request-entitys-media-type-text-plain-is-not-supported-for-this-resource
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
#if DEBUG
            // ��� ������� ������� JSON, ������� ��� ��������
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .Formatting = Newtonsoft.Json.Formatting.Indented;
#else
            // � ������ ���������� ���������� ������ ��� ����������� �������
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .Formatting = Newtonsoft.Json.Formatting.None;
#endif
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);         
        }
    }
}

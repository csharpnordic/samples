using NLog;
using System.Web.Http;

namespace GenericApi.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary> 
    [Attributes.ExceptionLogging()]
    public abstract class RootController : ApiController
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        protected static readonly Logger log = LogManager.GetCurrentClassLogger();       
    }
}
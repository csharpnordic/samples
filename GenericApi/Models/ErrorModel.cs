using Newtonsoft.Json;
using NLog;

namespace GenericApi.Models
{
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public abstract class ErrorModel
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        protected static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Успешное завершение операции
        /// <para>Не для интерфейса</para>
        /// </summary>
        [JsonIgnore()]
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}
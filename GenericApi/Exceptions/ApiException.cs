using System;

namespace GenericApi.Exceptions
{
    /// <summary>
    /// Исключение, специфичное для приложения
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Исключение с сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        public ApiException(string message) : base(message) { }
    }
}

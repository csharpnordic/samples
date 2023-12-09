using GenericApi.Exceptions;
using NLog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace GenericApi.Attributes
{
    /// <summary>
    /// Обработка исключений в контроллерах
    /// </summary>
    public class ExceptionLoggingAttribute : Attribute, IExceptionFilter
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Только один экземпляр атрибута 
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        /// Обработка искючений в контроллерах
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            try
            {
                Exception ex = context.Exception;

                // Протоколирование неожиданного исключения
                if (!(ex is ApiException) &&
                    !(ex is System.Data.Entity.Validation.DbEntityValidationException) &&
                    !(ex is OperationCanceledException))
                {
                    // Трассировка запроса при ошибке
                    log.Trace($"{context.Request.GetRequestContext().Principal.Identity.Name} ; {context.Request.Method.Method} {context.Request.RequestUri.LocalPath}{context.Request.RequestUri.Query} {context.Request.Content.Headers.ContentType}");
                    log.Fatal(ex);
                }

                // Формирование объекта по умолчанию
                object result = null;
                Type returnType = context.ActionContext.ActionDescriptor.ReturnType;

                string message;
                // Для действий, которые возвращают строку, возвращается сообщение об ошибке
                // Используем самое внутреннее исключение
                var exception = ex;
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
                if (exception is ApiException ||
                    exception is OperationCanceledException)
                {
                    message = exception.Message; // Используем сообщение из исключения - там оно человеческое
                    log.Info(message);
                }
                else if (ex is System.Data.Entity.Validation.DbEntityValidationException dbex)
                {
                    // Подробное протоколирование ошибок валидации модели EF при сохранении
                    var messages = new Models.MessageList();
                    foreach (var error in dbex.EntityValidationErrors)
                    {
                        foreach (var msg in error.ValidationErrors)
                        {
                            log.Debug($"{error.Entry.Entity.GetType()}.{msg.PropertyName}: {msg.ErrorMessage}");
                            messages.Add(msg.ErrorMessage);
                        }
                    }
                    message = messages.ToString();                  
                }
                else
                {
                    message = $"Произошла внутренняя ошибка системы: {exception.Message}";
                    log.Fatal(ex);
                }

                switch (returnType.Name)
                {
                    case nameof(String): // Первый вариант - строка с ошибкой или null
                        result = message;
                        break;

                    default:
                        if (returnType.IsInterface) break; // интерфейс нельзя создать

                        result = Activator.CreateInstance(returnType);
                        if (result is Models.ErrorModel error)
                        {
                            // Второй вариант - объект, где есть сообщение об ошибке
                            error.ErrorMessage = message;
                        }
                        break;
                }
                context.Response = context.Request.CreateResponse(result);

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                return Task.FromResult<object>(null);
            }
        }
    }
}
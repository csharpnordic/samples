using GenericApi.Exceptions;
using GenericApi.Models;
using GenericApi.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GenericApi.Controllers
{
    /// <summary>
    /// Сведения о продукции
    /// </summary>
    public class ProductionController : RootController
    {
        /// <summary>
        /// Нормативные сроки годности
        /// </summary>
        private List<Production> Productions = new List<Production>()
        {
            new Production("milk", "Молоко", 7),
            new Production("water", "Вода негазированная", 14),
            new Production("cheese", "Сыр брянский хороший", 7)
        };

        /// <summary>
        /// Наименование продукии по коду
        /// </summary>
        /// <param name="productionCode">Код продукции</param>
        /// <returns></returns>
        /// https://localhost:44313/api/production?productionCode= - плановое сообщение об ошибке
        /// https://localhost:44313/api/production?productionCode=milk - нормальная работа
        /// https://localhost:44313/api/production?productionCode=unknown - обработка исключения
        [HttpGet()]
        [Route("api/production")]
        public string ProductionName(string productionCode)
        {
            if (string.IsNullOrEmpty(productionCode))
            {
                throw new ApiException("Код продукции не задан");
            }
            return Productions.First(x=>x.Code == productionCode).Name;
        }

        /// <summary>
        /// Получение срока годности
        /// </summary>
        /// <param name="productionCode">Код продукции</param>
        /// <returns></returns>
        /// Варианты запуска:
        /// https://localhost:44313/api/expiration?productionCode= - плановое сообщение об ошибке
        /// https://localhost:44313/api/expiration?productionCode=milk - нормальная работа
        /// https://localhost:44313/api/expiration?productionCode=unknown - обработка исключения
        [HttpGet()]
        [Route("api/expiration")]
        public Models.ExpirationModel GetExpiration(string productionCode)
        {          
            if (string.IsNullOrEmpty(productionCode))
            {
                return new ExpirationModel("Код продукции не задан");
            }
            int duration = Productions.First(x => x.Code == productionCode).Duration;

            DateTime now = DateTime.Today;
            return new ExpirationModel()
            {
                ExpirationStart = now,
                ExpirationFinish = now.AddDays(duration)
            };
        }
    }
}
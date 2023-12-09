using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericApi.Models
{
    /// <summary>
    /// Срок годности продукции
    /// </summary>
    public class ExpirationModel : ErrorModel
    {
        /// <summary>
        /// Дата начала срока годности
        /// </summary>
        public DateTime? ExpirationStart { get; set; }

        /// <summary>
        /// Дата окончания срока годности
        /// </summary>
        public DateTime? ExpirationFinish { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ExpirationModel() { }

        /// <summary>
        /// Конструктор в случае ошибки
        /// </summary>
        /// <param name="message"></param>
        public ExpirationModel(string message) => ErrorMessage = message;        
    }
}


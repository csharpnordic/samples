using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericApi.Storage
{
    /// <summary>
    /// Продукция
    /// </summary>
    public class Production
    {
        /// <summary>
        /// Код продукции
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Наименование продукции
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Длительность срока годности продукции
        /// </summary>
        public int Duration { get; set; }

        public Production() { }

        public Production(string code, string name, int duration)
        {
            Code = code;
            Name = name;
            Duration = duration;
        }
    }
}
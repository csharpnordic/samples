using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class Core
    {
        /// <summary>
        /// Десериализация JSON-файла
        /// </summary>
        /// <param name="fileName">Имя файла JSON</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T LoadJson<T>(string fileName) where T : new()
        {
            // Чтение всего файла в одну строку
            string json = System.IO.File.ReadAllText(fileName);
            // Десериализация
            return (T?)System.Text.Json.JsonSerializer.Deserialize(json, typeof(T)) ?? new T();
        }
    }
}

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
        public static T LoadJson<T>(string fileName)
            => (T?)LoadJson(fileName, typeof(T));

        /// <summary>
        /// Десериализация JSON-файла
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object LoadJson(string fileName, Type type)
        {
            // Чтение всего файла в одну строку
            string json = System.IO.File.ReadAllText(fileName);
            // Десериализация
            var result = System.Text.Json.JsonSerializer.Deserialize(json, type);
            // Проверка на наличие объекта
            if (result == null)
            {
                // Создание пустого объекта заданного класса по умолчанию
                result = Activator.CreateInstance(type)!;
            }
            return result;
        }
    }
}

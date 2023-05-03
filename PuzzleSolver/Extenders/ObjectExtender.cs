using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace PuzzleSolver.Extenders
{
    public static class ObjectExtender
    {
        /// <summary>
        /// Сохранение объекта в файл в формате JSON
        /// </summary>
        /// <param name="o">Объект</param>
        /// <param name="fileName">Имя файла</param>
        public static void SaveJson(this object o, string fileName)
        {
            // Настройка сериализации
            System.Text.Json.JsonSerializerOptions options = new()
            {
                // Человекочитаемый JSON
                WriteIndented = true,
                // Запрет на перекодировку кириллицы и латиницы в unicode-представление символов
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };

            // Сериализация объекта в JSON
            string json = System.Text.Json.JsonSerializer.Serialize(o, o.GetType(), options);

            // Запись строки в файл
            System.IO.File.WriteAllText(fileName, json);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    /// <summary>
    /// Общие методы для проекта 
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Создание прямоугольного массива массивов заданного типа
        /// </summary>
        /// <typeparam name="T">Тип элемента массиа</typeparam>
        /// <param name="sizeX">Размер массива по горизонтали</param>
        /// <param name="sizeY">Размер массива по вертикали</param>
        /// <param name="initialize">Требуется ли создавать объекты - элементы массива</param>
        /// <returns></returns>
        public static T[][] Array2<T>(int sizeX, int sizeY, bool initialize) where T : new()
        {
            T[][] array = new T[sizeX][];

            for (int x = 0; x < sizeX; x++)
            {
                array[x] = new T[sizeY];
                if (initialize)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        array[x][y] = new T();
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// Создание прямоугольного массива массивов массивов заданного типа
        /// </summary>
        /// <typeparam name="T">Тип элемента массиа</typeparam>
        /// <param name="sizeX">Размер массива по горизонтали</param>
        /// <param name="sizeY">Размер массива по вертикали</param>
        /// <param name="sizeZ">Размер массива по третьей координате</param>
        /// <param name="initialize">Требуется ли создавать объекты - элементы массива</param>
        /// <returns></returns>
        public static T[][][] Array3<T>(int sizeX, int sizeY, int sizeZ, bool initialize) where T : new()
        {
            T[][][] array = new T[sizeX][][];

            for (int x = 0; x < sizeX; x++)
            {
                array[x] = Array2<T>(sizeY, sizeZ, initialize);
            }

            return array;
        }


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

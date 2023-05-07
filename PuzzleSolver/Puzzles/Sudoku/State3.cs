using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Sudoku
{
    /// <summary>
    /// Состояние для треугольного судоку
    /// </summary>
    public class State3 : IPossibleMove
    {
        /// <summary>
        /// Полное имя класса
        /// </summary>
        public string ClassName => GetType().FullName;

        /// <summary>
        /// Размер игрового поля 
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Игровое поле
        /// <para>Горизонталь - вертикаль - третья координата (верхний/нижний треугольник)</para>
        /// </summary>
        public Cell[][][] Cells { get; set; }

        /// <summary>
        /// Изображения клеток
        /// </summary>
        public string[] Images { get; set; }

        /// <summary>
        /// Группировки клеток поля для проверки уникальности
        /// <para>Горизонтали, диагонали, квадраты <seealso cref="Size"/>*<seealso cref="Size"/></para>
        /// </summary>
        [JsonIgnore]
        public List<Cell[]> Lines { get; set; }

        /// <summary>
        /// Беспараметрический конструктор для сериализации
        /// </summary>
        public State3() { }

        /// <summary>
        /// Конструктор по размеру игрового поля
        /// </summary>
        /// <param name="size">Размер игрового поля</param>
        /// <param name="numbers">Число вариантов значений</param>
        public State3(int size, int numbers)
        {
            Size = size;

            Cells = Solver.Array3<Cell>(size, size, 2, true);
            Images = new string[numbers + 1];

            // Инициализация обозначений клеток по умолчанию
            Images[0] = " ";
            for (int i = 1; i < Images.Length; i++)
            {
                Images[i] = i.ToString();
            }
        }

        /// <summary>
        /// Проверка хода на корректность
        /// </summary>
        /// <param name="imove"></param>
        /// <returns></returns>
        public bool PossibleMove(IMove imove)
        {
            if (!(imove is Move move)) return false;
            return false;
        }
    }
}

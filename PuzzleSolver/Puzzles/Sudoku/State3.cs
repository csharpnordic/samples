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

            /*
            Images[1] = "\U0001F41F"; // рыба
            Images[2] = "\U000026F0"; // гора
            Images[3] = "\U0001F480"; // череп
            Images[4] = "\U00002197"; // стрелка 
            Images[5] = "\U0001F334"; // пальма
            Images[6] = "@"; // спираль
            Images[7] = "\U0001F41A"; // ракушка
            Images[8] = "\U0001F41E"; // божья коровка (жучок)
            Images[9] = "\U0001f99c"; // птица
            */
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

using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    public class State : IState
    {
        /// <summary>
        /// Полное имя класса
        /// </summary>
        public string ClassName => GetType().FullName;

        /// <summary>
        /// Размер игрового поля по горизонтали
        /// </summary>
        public int SizeX { get; set; }

        /// <summary>
        /// Размер игрового поля по вертикали
        /// </summary>
        public int SizeY { get; set; }

        /// <summary>
        /// Количество цветов маршрутов
        /// </summary>
        public int Colors { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[][] Field { get; set; }

        /// <summary>
        /// Граница игрового поля
        /// <para>Первое измерение массива - <seealso cref="Side"/></para>
        /// </summary>
        public Cell[][] Border { get; set; }

        /// <summary>
        /// Набор плиток
        /// </summary>
        public List<Tile> TileSet { get; set; }

        /// <summary>
        /// Беспараметрический конструктор для сериализации
        /// </summary>
        public State() { }

        /// <summary>
        /// Граничная плитка
        /// </summary>
        /// <param name="side">Сторона границы</param>
        /// <param name="index">Индекс плитки, начиная с 0</param>
        /// <returns></returns>
        public Cell this[Side side, int index]
        {
            get => Border[(int)side][index];
        }

        /// <summary>
        /// Конструктор по размеру игрового поля
        /// </summary>
        /// <param name="sizeX">Размер игрового поля по горизонтали</param>
        /// <param name="sizeY">Размер игрового поля по вертикали</param>
        public State(int sizeX, int sizeY, int colors)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Colors = colors;

            // Создание игрового поля
            Field = Solver.Array2<Cell>(sizeX, sizeY, true);

            // Создание границ игрового поля
            Border = new Cell[4][];
            Border[(int)Side.Left] = new Cell[sizeY];
            Border[(int)Side.Right] = new Cell[sizeY];
            Border[(int)Side.Top] = new Cell[sizeX];
            Border[(int)Side.Bottom] = new Cell[sizeX];
            for (int i = 0; i <= Border.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= Border[i].GetUpperBound(0); j++)
                {
                    Border[i][j] = new Cell()
                    {
                        Border = true,
                        Tile = new Tile()
                        {
                            Side = OppositeSide((Side)i)
                        }
                    };
                };
            }

            // Набор фигур
            TileSet = new();
        }

        /// <summary>
        /// Сторона, противоположная заданной
        /// </summary>
        /// <param name="side">Сторона плитки</param>
        /// <returns>Сторона, противоположная заданной</returns>
        /// <exception cref="Exception"></exception>
        private Side OppositeSide(Side side)
        {
            switch (side)
            {
                case Side.Left: return Side.Right;
                case Side.Right: return Side.Left;
                case Side.Top: return Side.Bottom;
                case Side.Bottom: return Side.Top;
                default: throw new Exception();
            }
        }

        /// <summary>
        /// Список возможных ходов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMove> GetMoves()
        {
            return new List<Move>();
        }

        /// <summary>
        /// Ход
        /// </summary>
        /// <param name="move"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Move(IMove move)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Отмена хода
        /// </summary>
        /// <param name="move"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UndoMove(IMove move)
        {
            throw new NotImplementedException();
        }

        public bool Done()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Протоколирование
        /// </summary>
        public void Log()
        {
        }
    }
}

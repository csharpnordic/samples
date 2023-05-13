using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    public class State : IState
    {
        /// <summary>
        /// Соседние клетки
        /// </summary>
        private static readonly Neighbour[] Neighbours = new Neighbour[]
        {
            new Neighbour(-1, 0, Side.Left),
            new Neighbour(0, -1, Side.Top),
            new Neighbour(1,  0, Side.Right),
            new Neighbour(0,  1, Side.Bottom)
        };

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
        /// Граничная клетка
        /// </summary>
        /// <param name="side">Сторона границы</param>
        /// <param name="index">Индекс клетки, начиная с 0</param>
        /// <returns></returns>
        public Cell this[Side side, int index]
        {
            get => Border[(int)side][index];
        }

        /// <summary>
        /// Клетка игрового поля, включая граничные
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0) return this[Side.Left, y];
                if (y < 0) return this[Side.Top, x];
                if (x >= SizeX) return this[Side.Right, y];
                if (y >= SizeY) return this[Side.Bottom, x];
                return Field[x][y];
            }
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
        private static Side OppositeSide(Side side)
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
        /// Заполнение набора возможных фигур
        /// <para>Все фигуры, кроме зафиксированных, удаляются с поля и помещаются в набор фигур</para>
        /// </summary>
        public void InitTileSet()
        {
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    if (!Field[x][y].Fixed)
                    {
                        TileSet.Add(Field[x][y].Tile);
                        Field[x][y].Tile = null;
                    }
                }
            }
        }

        /// <summary>
        /// Поиск первой свободной клетки
        /// </summary>
        /// <param name="xCell"></param>
        /// <param name="yCell"></param>
        /// <returns></returns>
        private bool FindEmplyCell(out int xCell, out int yCell)
        {
            xCell = -1;
            yCell = -1;
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    if (Field[x][y].Tile == null)
                    {
                        xCell = x;
                        yCell = y;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка хода на корректность
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool PossibleMove(int x, int y, Tile tile)
        {
            foreach (var neighbour in Neighbours)
            {
                var cell = this[x + neighbour.DX, y + neighbour.DY];
                if (cell.Tile == null) continue; // пропуск пустых клеток
                if (cell.Tile[OppositeSide(neighbour.Side)] != tile[neighbour.Side])
                {
                    // Если не совпадают соединения на прилегающих к друг другу сторонах,
                    // ход невозможен
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Список возможных ходов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMove> GetMoves()
        {
            List<Move> moves = new();

            // Поиск свободной клетки
            if (!FindEmplyCell(out int xCell, out int yCell))
            {
                return moves; // если свободной ячейки нет, возвращаем пустой список
            }

            // Перебираем все оставшиеся плитки
            foreach (var tile in TileSet)
            {
                if (PossibleMove(xCell, yCell, tile))
                {
                    Move move = new Move(Field[xCell][yCell], tile);
                    moves.Add(move);
                }
            }

            return moves;
        }

        /// <summary>
        /// Ход
        /// </summary>
        /// <param name="move"></param>        
        public void Move(IMove imove)
        {
            if (imove is Move move)
            {
                move.Cell.Tile = move.Tile;              
                TileSet.Remove(move.Tile);
            }
        }

        /// <summary>
        /// Отмена хода
        /// </summary>
        /// <param name="move"></param>      
        public void UndoMove(IMove imove)
        {
            if (imove is Move move)
            {
                TileSet.Add(move.Tile);
                move.Cell.Tile = null;             
            }
        }

        public bool Done()
        {         
            return !FindEmplyCell(out int _, out int _);
        }

        /// <summary>
        /// Протоколирование
        /// </summary>
        public void Log()
        {
        }
    }
}

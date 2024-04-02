using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Головоломка построения карты
    /// </summary>
    public class State : BaseState, IState
    {
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
        internal List<Tile>? TileSet { get; set; }

        /// <summary>
        /// Набор цветов (строковый эквивалент)
        /// </summary>
        public string[] Color { get; set; }

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
        /// <para>Если контроль границ отключён, вместо граничной клетки возвращается null</para>
        /// </summary>
        /// <param name="x">Абсцисса клетки, начиная с 0. -1 для левой границы. <seealso cref="SizeX"/> для правой границы</param>
        /// <param name="y">Ордината клетки, начиная с 0. -1 для верхней границы. <seealso cref="SizeY"/> для нижней границы</param>
        /// <returns></returns>
        public Cell? this[int x, int y]
        {
            get
            {
                if (x < 0) return CheckBorders ? this[Side.Left, y] : null;
                if (y < 0) return CheckBorders ? this[Side.Up, x] : null;
                if (x >= SizeX) return CheckBorders ? this[Side.Right, y] : null;
                if (y >= SizeY) return CheckBorders ? this[Side.Down, x] : null;
                return Field[x][y];
            }
        }

        /// <summary>
        /// Конструктор по параметрам
        /// </summary>
        /// <param name="state">Начальные параметры головоломки</param>
        public State(BaseState state)
        {
            // Копирование параметров
            SizeX = state.SizeX;
            SizeY = state.SizeY;
            Colors = state.Colors;
            CheckBorders = state.CheckBorders;
            MultiColor = state.MultiColor;

            // Массив цветов
            Color = new string[Colors];

            // Создание игрового поля
            Field = Core.Array2<Cell>(SizeX, SizeY, true);

            // Создание границ игрового поля
            Border = new Cell[4][];
            Border[(int)Side.Left] = new Cell[SizeY];
            Border[(int)Side.Right] = new Cell[SizeY];
            Border[(int)Side.Up] = new Cell[SizeX];
            Border[(int)Side.Down] = new Cell[SizeX];
            for (int i = 0; i <= Border.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= Border[i].GetUpperBound(0); j++)
                {
                    Border[i][j] = new Cell()
                    {
                        Border = true,
                        Tile = new Tile()
                        {
                            Side = Solver.OppositeSide((Side)i)
                        }
                    };
                };
            }
        }

        /// <summary>
        /// Заполнение набора возможных фигур
        /// <para>Все фигуры, кроме зафиксированных, удаляются с поля и помещаются в набор фигур</para>
        /// </summary>
        public void InitTileSet()
        {
            if (TileSet == null)
            {
                TileSet = new();
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
            foreach (var neighbour in Neighbour.Neighbours)
            {
                var cell = this[x + neighbour.DX, y + neighbour.DY];
                if (cell?.Tile == null) continue; // пропуск пустых или незначащих клеток
                if (cell.Tile[Solver.OppositeSide(neighbour.Side)] != tile[neighbour.Side])
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
                    Move move = new (Field[xCell][yCell], tile);
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

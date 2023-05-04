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
    /// Состояние игрового поля
    /// </summary>
    public class State : IState
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Размер игрового поля по горизонтали
        /// </summary>
        public int SizeX { get; set; }

        /// <summary>
        /// Размер игрового поля по вертикали
        /// </summary>
        public int SizeY { get; set; }

        /// <summary>
        /// Размер квадрата
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[][] Cells { get; set; }

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
        public State() { }

        /// <summary>
        /// Конструктор по размеру игрового поля
        /// </summary>
        /// <param name="sizeX">Размер игрового поля по горизонтали</param>
        /// <param name="sizeY">Размер игрового поля по вертикали</param>
        /// <param name="size">Размер квадрата</param>
        public State(int sizeX, int sizeY, int size)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Size = size;
            Cells = Solver.Array2<Cell>(sizeX, sizeY, true);
            Images = new string[Size * Size + 1];

            // Инициализация обозначений клеток по умолчанию
            Images[0] = " ";
            for (int i = 1; i < Images.Length; i++)
            {
                Images[i] = i.ToString();
            }

            InitLines();
        }

        /// <summary>
        /// Инициализация группировок клеток
        /// </summary>
        public void InitLines()
        {
            // Список групп клеток
            Lines = new List<Cell[]>();

            // Горизонтали
            for (int y = 0; y < SizeY; y++)
            {
                Cell[] cells = new Cell[SizeX];
                for (int x = 0; x < SizeX; x++)
                {
                    cells[x] = Cells[x][y];
                }
                Lines.Add(cells);
            }

            // Вертикали
            for (int x = 0; x < SizeX; x++)
            {
                Cell[] cells = new Cell[SizeY];
                for (int y = 0; y < SizeY; y++)
                {
                    cells[y] = Cells[x][y];
                }
                Lines.Add(cells);
            }

            // Группы клеток Size*Size
            for (int x = 0; x < SizeX; x += Size)
            {
                for (int y = 0; y < SizeY; y += Size)
                {
                    Cell[] cells = new Cell[Size * Size];
                    int index = 0;
                    for (int dx = 0; dx < Size; dx++)
                    {
                        for (int dy = 0; dy < Size; dy++)
                        {
                            cells[index++] = Cells[x + dx][y + dy];
                        }
                    }
                    Lines.Add(cells);
                }
            }
        }

        /// <summary>
        /// Перечень возможных ходов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMove> GetMoves()
        {
            List<Move> result = new();
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    // Пропуск ячеек со значениями
                    if (Cells[x][y].Number > 0) continue;

                    List<Move> moves = new();
                    // Перебор всех возможных чисел
                    for (int number = 1; number <= Size * Size; number++)
                    {
                        Move move = new Move(Cells[x][y], number);
                        if (PossibleMove(move))
                        {
                            moves.Add(move);
                        }
                    }
                    // выбираем клетку с минимально возможным количеством вариантов существующих ходов
                    if (moves.Count > 0 && (result.Count == 0 || result.Count > moves.Count))
                    {
                        result = moves;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Проверка хода на корректность
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool PossibleMove(Move move)
        {
            // Проверяем группы клеток, в которые входит клетка хода
            foreach (var line in Lines.Where(x => x.Contains(move.Cell)))
            {
                // проверка на неуникальность номера (исключая из проверки саму клетку)
                if (line.Where(x => x != move.Cell).Any(x => x.Number == move.Number)) return false;
            }
            return true;
        }

        /// <summary>
        /// Построение нового состояния после хода
        /// </summary>
        /// <param name="move">Возможный ход</param>
        /// <returns></returns>
        public void Move(IMove imove)
        {
            if (imove is Move move)
            {
                move.Cell.Number = move.Number;
                move.Cell.Fixed = true;
            }
        }

        /// <summary>
        /// Возврат к предыдущему состоянию после отмены хода
        /// </summary>
        /// <param name="move">Возможный ход</param>
        /// <returns></returns>
        public void UndoMove(IMove imove)
        {
            if (imove is Move move)
            {
                move.Cell.Number = 0;
                move.Cell.Fixed = false;
            }
        }

        /// <summary>
        /// Решение найдено - все клетки заполнены ненулевыми числами
        /// </summary>
        /// <returns></returns>
        public bool Done() => Cells.All(x => x.All(x => x.Number > 0));

        /// <summary>
        /// Протоколирование
        /// </summary>
        public void Log()
        {
            log.Trace(string.Join("", Enumerable.Repeat("-", SizeX + SizeX / Size - 1)));
            for (int y = 0; y < SizeY; y++)
            {
                if (y > 0 && y % Size == 0)
                {
                    log.Trace(string.Empty);
                }
                string s = "";
                for (int x = 0; x < SizeX; x++)
                {
                    if (x > 0 && x % Size == 0) s += ' ';
                    s += Cells[x][y].Number > 0 ? Cells[x][y].Number.ToString() : ".";
                }
                log.Trace(s);
            }
        }
    }
}
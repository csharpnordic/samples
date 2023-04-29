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
            Cells = new Cell[SizeX][];

            for (int x = 0; x < SizeX; x++)
            {
                Cells[x] = new Cell[SizeY];
                for (int y = 0; y < SizeY; y++)
                {
                    Cells[x][y] = new Cell();
                }
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
        public IEnumerable<Move> GetMoves()
        {
            List<Move> result = new();
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    // Пропуск фиксированных ячеек
                    if (Cells[x][y].Fixed) continue;

                    List<Move> moves = new List<Move>();

                    // Перебор всех возможных чисел
                    for (int number = 1; number <= Size * Size; number++)
                    {
                        Move move = new Move()
                        {
                            Number = number,
                            Cell = Cells[x][y]
                        };
                        if (PossibleMove(move))
                        {
                            moves.Add(move);
                        }
                    }
                    // выбираем клетку с минимально возможным количеством вариантов ходов
                    if (result.Count() == 0 || result.Count() > moves.Count())
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
                // проверка на неуникальность номера
                if (line.Any(x => x.Number == move.Number)) return false;
            }
            return true;
        }

        /// <summary>
        /// Построение нового состояния после хода
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public State Move(Move move)
        {
            move.Cell.Number = move.Number;
            move.Cell.Fixed = true;
            return this;
        }

        /// <summary>
        /// Решение найдено
        /// </summary>
        /// <returns></returns>
        public bool Done() => Cells.All(x => x.All(x => x.Fixed));
    }
}
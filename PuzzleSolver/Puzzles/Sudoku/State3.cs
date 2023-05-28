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
    public class State3 : IState
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
        public List<List<Cell>> Lines { get; set; }

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

            Cells = Core.Array3<Cell>(size, size, 2, true);
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

            InitLines();
        }

        /// <summary>
        /// Инициализация группировок клеток
        /// </summary>
        public void InitLines()
        {
            // Список групп клеток
            Lines = new();

            // Вертикали           
            for (int x = 0; x < Size; x++)
            {
                var line = new List<Cell>();
                for (int y = 0; y < Size - x; y++)
                {
                    for (int z = 0; z <= 1; z++)
                    {
                        // пропуск верхнего треугольника за границей
                        if (x + y + z >= Size) continue;

                        line.Add(Cells[x][y][z]);
                    }
                }
                Lines.Add(line);
            }

            // Горизонтали
            for (int y = 0; y < Size; y++)
            {
                var line = new List<Cell>();
                for (int x = 0; x < Size - y; x++)
                {
                    for (int z = 0; z <= 1; z++)
                    {
                        // пропуск верхнего треугольника за границей
                        if (x + y + z >= Size) continue;

                        line.Add(Cells[x][y][z]);
                    }
                }
                Lines.Add(line);
            }

            // Диагонали        
            for (int n = 0; n < Size; n++)
            {
                var line = new List<Cell>();

                int y = 0;
                int x = n - y;
                int z = 0;
                while (x > 0 || z > 0)
                {
                    line.Add(Cells[x][y][z]);
                    if (z == 0)
                    {
                        z = 1;
                        x--;
                    }
                    else // z == 1
                    {
                        z = 0;
                        y++;
                    }
                }

                Lines.Add(line);
            }
        }

        /// <summary>
        /// Проверка хода на корректность
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool PossibleMove(IMove imove)
        {
            if (!(imove is Move move)) return false;
            // Проверяем группы клеток, в которые входит клетка хода
            foreach (var line in Lines.Where(x => x.Contains(move.Cell)))
            {
                // проверка на неуникальность номера (исключая из проверки саму клетку)
                if (line.Where(x => x != move.Cell).Any(x => x.Number == move.Number)) return false;
            }
            return true;
        }

        public IEnumerable<IMove> GetMoves()
        {
            List<Move> result = new();
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size - x; y++)
                {
                    for (int z = 0; z <= 1; z++)
                    {
                        // пропуск верхнего треугольника за границей
                        if (x + y + z >= Size) continue;
                        // Пропуск ячеек со значениями
                        if (Cells[x][y][z].Number > 0) continue;

                        List<Move> moves = new();
                        // Перебор всех возможных чисел
                        for (int number = 1; number < Images.Length; number++)
                        {
                            Move move = new Move(Cells[x][y][z], number);
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
            }
            return result;
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
                // move.Cell.Fixed = true;
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
                // move.Cell.Fixed = false;
            }
        }

        /// <summary>
        /// Решение найдено - все клетки заполнены ненулевыми числами
        /// </summary>
        /// <returns></returns>
        public bool Done()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size - x; y++)
                {
                    for (int z = 0; z <= 1; z++)
                    {
                        // пропуск верхнего треугольника за границей
                        if (x + y + z >= Size) continue;
                        // Найдена незаполненная ячейка
                        if (Cells[x][y][z].Number == 0) return false;
                    }
                }
            }
            return true;
        }       

        /// <summary>
        /// Протоколирование
        /// </summary>
        public void Log()
        {
        }
    }
}

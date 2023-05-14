using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Головоломка построения маршрута
    /// </summary>
    public class StateT : IState
    {
        /// <summary>
        /// Протоколирование
        /// </summary>
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Полное имя класса
        /// </summary>
        public string ClassName => GetType().FullName;

        /// <summary>
        /// Стек ходов
        /// </summary>
        private Stack<MoveT> moves = new();

        /// <summary>
        /// Начальная точка
        /// </summary>
        private Point? start = null;

        /// <summary>
        /// Конечная точка
        /// </summary>
        private Point? finish = null;

        /// <summary>
        /// Размер игрового поля по горизонтали
        /// </summary>
        public int SizeX { get; set; }

        /// <summary>
        /// Размер игрового поля по вертикали
        /// </summary>
        public int SizeY { get; set; }

        /// <summary>
        /// Максимальное количество ходов
        /// </summary>
        public int MaxMoves { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[][] Field { get; set; }

        /// <summary>
        /// Клетка игрового поля по точке
        /// </summary>
        /// <param name="point">Точка (координаты)</param>
        /// <returns></returns>
        public Cell this[Point point]
        {
            get => Field[point.X][point.Y];
        }

        /// <summary>
        /// Беспараметрический конструктор для сериализации
        /// </summary>
        public StateT() { }

        /// <summary>
        /// Конструктор по размеру игрового поля
        /// </summary>
        /// <param name="sizeX">Размер игрового поля по горизонтали</param>
        /// <param name="sizeY">Размер игрового поля по вертикали</param>
        public StateT(int sizeX, int sizeY, int maxMoves)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            MaxMoves = maxMoves;

            // Создание игрового поля
            Field = Solver.Array2<Cell>(sizeX, sizeY, true);
        }

        /// <summary>
        /// Поиск клетки игрового поля по типу
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Point? FindCell(CellType type)
        {
            for (int x = 0; x < Field.Length; x++)
            {
                for (int y = 0; y < Field[x].Length; y++)
                {
                    if (Field[x][y].Type == type)
                    {
                        return new Point(x, y);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Проверка на корректность хода
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        private bool PossibleMove(MoveT move)
        {
            // проверка на нахождение в пределах поля
            if (move.To.X < 0 || move.To.Y < 0) return false;
            if (move.To.X >= SizeX || move.To.Y >= SizeY) return false;

            // проверка на повторный вход в ту же точку
            if (moves.Any(x => x.From == move.To)) return false; // тут мы уже были           

            // проверка наличия выездной дороги
            int wayOut = this[move.From].Tile[move.From.Side];
            if (wayOut == 0) return false; // нет выездной дороги

            // проверка наличия въездной дороги
            int wayIn = this[move.To].Tile[Solver.OppositeSide(move.To.Side)];
            if (wayIn == 0) return false; //нет въездной дороги

            // можно проехать, всё хорошо
            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<IMove> GetMoves()
        {
            var result = new List<IMove>();

            // Поиск начала маршрута
            if (start == null)
            {
                start = FindCell(CellType.Start);
            }

            // Поиск окончаия маршрута
            if (finish == null)
            {
                finish = FindCell(CellType.Finish);
            }

            // Проверка на исчерпание количества ходов
            if (moves.Count == MaxMoves)
            {
                return result;
            }

            Car current; // текущее положение машинки
            // Проверка на первый ход
            if (moves.Count == 0)
            {
                current = new Car()
                {
                    X = start.X,
                    Y = start.Y,
                    Side = Side.Right // по умолчанию едем сначала направо
                };
                // отметим стартовую точку
                this[current].Mark = true;
            }
            else
            {
                current = moves.Peek().To;
            }

            // Перебор возможных направлений
            foreach (var turn in Enum.GetValues<Turn>())
            {
                var next = current.Move(turn);
                var move = new MoveT(current, turn, next);
                if (PossibleMove(move))
                {
                    result.Add(move);
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public void Move(IMove imove)
        {
            if (imove is MoveT move)
            {
                this[move.To].Mark = true;
                moves.Push(move);
                log.Trace($"+ {moves.Count,2}: {move}");
            }
        }

        /// <inheritdoc/>
        public void UndoMove(IMove imove)
        {
            if (moves.Peek() == imove)
            {
                var move = moves.Pop();
                this[move.To].Mark = false;
                log.Trace($"- {moves.Count,2}: {move}");
            }
            else
            {
                throw new Exception();
            }
        }

        /// <inheritdoc/>
        public bool Done()
        {
            // Последний сделанный ход
            var move = (MoveT)moves.Peek();

            // Если приехали в заданную точку (с любого направления), то ок
            return finish.X == move.To.X && finish.Y == move.To.Y;
        }

        /// <inheritdoc/>
        public void Log()
        {
        }

        /// <inheritdoc/>
        public void LogSolution()
        {
            log.Info("Найденное решение:");
            foreach (var move in moves)
            {
                log.Info($"* {moves.Count,2}: {move}");
            }
        }
    }
}
